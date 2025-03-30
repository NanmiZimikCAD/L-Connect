using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Data;
using L_Connect.Models.Domain;
using L_Connect.Models.PricingService;
using L_Connect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L_Connect.Services.Implementations
{
    public class PricingService : IPricingService
    {
        private readonly ApplicationDbContext _context;
        
        // Dictionary to cache distances between locations
        private static readonly Dictionary<string, decimal> DistanceCache = new Dictionary<string, decimal>
        {
            { "USA-Canada", 500m },
            { "USA-Brazil", 7500m },
            { "China-Canada", 12000m },
            { "Brazil-China", 18000m },
            { "Shenzhen-Sao Paulo", 17500m },
            { "Shenzhen-Toronto", 12000m },
            { "Shenzhen-Vancouver", 10500m },
            { "Shenzhen-Mexico City", 14000m },
            { "Shenzhen-Lima", 16000m },
            { "Shenzhen-Ushuaia", 19000m }
        };

        public PricingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RouteInfo>> GetAllRoutesAsync()
        {
            try
            {
                // Query pricing data from database
                var pricingData = await _context.Set<Pricing>()
                    .Where(p => p.IsActive)
                    .ToListAsync();

                // If no data in database, use temporary data
                if (!pricingData.Any())
                {
                    return GetTemporaryRouteData();
                }

                // Group by origin/destination
                var groupedData = pricingData
                    .GroupBy(p => new { p.OriginLocation, p.DestinationLocation })
                    .ToList();
                    
                var routes = new List<RouteInfo>();
                
                // Process each group separately to handle async operations
                foreach (var group in groupedData)
                {
                    var distance = await GetDistanceAsync(group.Key.OriginLocation, group.Key.DestinationLocation);
                    
                    routes.Add(new RouteInfo
                    {
                        Origin = group.Key.OriginLocation,
                        Destination = group.Key.DestinationLocation,
                        BasePrices = group.ToDictionary(
                            p => p.TransportationMethod,
                            p => p.BaseRate
                        ),
                        BulkThreshold = group.First().BulkThreshold,
                        BulkDiscountRate = group.First().BulkDiscountRate,
                        CustomServiceCharge = group.First().CustomServiceCharge,
                        InsuranceCharge = group.First().InsuranceCharge,
                        Distance = distance
                    });
                }

                return routes;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetAllRoutesAsync: {ex.Message}");
                return GetTemporaryRouteData();
            }
        }

        public async Task<List<string>> GetAvailableOriginsAsync()
        {
            try
            {
                var origins = await _context.Set<Pricing>()
                    .Where(p => p.IsActive)
                    .Select(p => p.OriginLocation)
                    .Distinct()
                    .ToListAsync();

                if (!origins.Any())
                {
                    // Return temporary data if database is empty
                    return GetTemporaryRouteData().Select(r => r.Origin).Distinct().ToList();
                }

                return origins;
            }
            catch
            {
                return GetTemporaryRouteData().Select(r => r.Origin).Distinct().ToList();
            }
        }

        public async Task<List<string>> GetAvailableDestinationsAsync(string origin = null)
        {
            try
            {
                IQueryable<Pricing> query = _context.Set<Pricing>().Where(p => p.IsActive);

                if (!string.IsNullOrEmpty(origin))
                {
                    query = query.Where(p => p.OriginLocation == origin);
                }

                var destinations = await query
                    .Select(p => p.DestinationLocation)
                    .Distinct()
                    .ToListAsync();

                if (!destinations.Any())
                {
                    // Return temporary data if database is empty
                    var tempRoutes = GetTemporaryRouteData();
                    return (origin == null 
                        ? tempRoutes.Select(r => r.Destination) 
                        : tempRoutes.Where(r => r.Origin == origin).Select(r => r.Destination))
                        .Distinct()
                        .ToList();
                }

                return destinations;
            }
            catch
            {
                var tempRoutes = GetTemporaryRouteData();
                return (origin == null 
                    ? tempRoutes.Select(r => r.Destination) 
                    : tempRoutes.Where(r => r.Origin == origin).Select(r => r.Destination))
                    .Distinct()
                    .ToList();
            }
        }

        public async Task<RouteInfo> GetRoutePricingAsync(string origin, string destination)
        {
            try
            {
                var pricingData = await _context.Set<Pricing>()
                    .Where(p => p.IsActive && p.OriginLocation == origin && p.DestinationLocation == destination)
                    .ToListAsync();

                if (!pricingData.Any())
                {
                    // Return from temporary data if not found in database
                    return GetTemporaryRouteData()
                        .FirstOrDefault(r => r.Origin == origin && r.Destination == destination);
                }

                return new RouteInfo
                {
                    Origin = origin,
                    Destination = destination,
                    BasePrices = pricingData.ToDictionary(
                        p => p.TransportationMethod,
                        p => p.BaseRate
                    ),
                    BulkThreshold = pricingData.First().BulkThreshold,
                    BulkDiscountRate = pricingData.First().BulkDiscountRate,
                    CustomServiceCharge = pricingData.First().CustomServiceCharge,
                    InsuranceCharge = pricingData.First().InsuranceCharge,
                    Distance = await GetDistanceAsync(origin, destination)
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in GetRoutePricingAsync: {ex.Message}");
                
                // Return from temporary data
                return GetTemporaryRouteData()
                    .FirstOrDefault(r => r.Origin == origin && r.Destination == destination);
            }
        }

        public async Task<Dictionary<string, decimal>> GetTransportationMethodRatesAsync(string origin, string destination)
        {
            var route = await GetRoutePricingAsync(origin, destination);
            return route?.BasePrices ?? new Dictionary<string, decimal>();
        }

        public async Task<QuoteCalculationResult> CalculateShippingCostAsync(
            string origin, 
            string destination, 
            decimal weight, 
            string transportMethod, 
            int quantity = 1, 
            bool needsCustomServices = false, 
            bool needsInsurance = false)
        {
            try
            {
                // Get route pricing information
                var routeInfo = await GetRoutePricingAsync(origin, destination);
                
                if (routeInfo == null)
                {
                    throw new InvalidOperationException($"No pricing found for route {origin} to {destination}");
                }

                // Ensure transportation method exists
                if (!routeInfo.BasePrices.ContainsKey(transportMethod))
                {
                    throw new InvalidOperationException($"Transportation method {transportMethod} not available for this route");
                }

                // Get base rate for selected transportation method
                decimal baseRate = routeInfo.BasePrices[transportMethod];
                
                // Calculate weight-based cost
                // For simplicity, assuming a fixed weight rate (you might want to get this from the database too)
                decimal weightRate = transportMethod == "Flight" ? 5.0m : 3.0m;
                decimal basePricePerKg = weightRate;
                
                // Calculate total weight
                decimal totalWeight = weight * quantity;
                
                // Base cost calculation
                decimal baseCost = baseRate;
                decimal weightCost = totalWeight * weightRate;
                decimal subtotal = baseCost + weightCost;
                
                // Apply bulk discount if applicable
                decimal discountPercentage = 0;
                decimal discountAmount = 0;
                
                if (quantity >= routeInfo.BulkThreshold)
                {
                    discountPercentage = routeInfo.BulkDiscountRate * 100;
                    discountAmount = subtotal * routeInfo.BulkDiscountRate;
                }
                
                // Add additional services costs
                decimal customServiceFee = needsCustomServices ? routeInfo.CustomServiceCharge * quantity : 0;
                decimal insuranceFee = needsInsurance ? routeInfo.InsuranceCharge * quantity : 0;
                
                // Calculate total
                decimal totalCost = subtotal - discountAmount + customServiceFee + insuranceFee;
                
                return new QuoteCalculationResult
                {
                    BaseCost = baseCost,
                    WeightCost = weightCost,
                    SubtotalBeforeDiscounts = subtotal,
                    DiscountPercentage = discountPercentage,
                    DiscountAmount = discountAmount,
                    CustomServiceCharge = customServiceFee,
                    InsuranceCharge = insuranceFee,
                    TotalCost = totalCost,
                    TransportationMethod = transportMethod,
                    Distance = routeInfo.Distance,
                    Quantity = quantity,
                    BasePricePerKg = basePricePerKg
                };
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error in CalculateShippingCostAsync: {ex.Message}");
                
                // Return a basic calculation using fixed values
                decimal baseRate = transportMethod == "Flight" ? 250m : 180m;
                decimal weightRate = transportMethod == "Flight" ? 5.0m : 3.0m;
                decimal baseCost = baseRate;
                decimal weightCost = weight * quantity * weightRate;
                decimal subtotal = baseCost + weightCost;
                
                decimal discountPercentage = 0;
                decimal discountAmount = 0;
                
                if (quantity >= 10) // Default bulk threshold
                {
                    discountPercentage = 10; // 10%
                    discountAmount = subtotal * 0.1m;
                }
                
                decimal customServiceFee = needsCustomServices ? 50m * quantity : 0;
                decimal insuranceFee = needsInsurance ? 20m * quantity : 0;
                
                decimal totalCost = subtotal - discountAmount + customServiceFee + insuranceFee;
                
                return new QuoteCalculationResult
                {
                    BaseCost = baseCost,
                    WeightCost = weightCost,
                    SubtotalBeforeDiscounts = subtotal,
                    DiscountPercentage = discountPercentage,
                    DiscountAmount = discountAmount,
                    CustomServiceCharge = customServiceFee,
                    InsuranceCharge = insuranceFee,
                    TotalCost = totalCost,
                    TransportationMethod = transportMethod,
                    Distance = await GetDistanceAsync(origin, destination),
                    Quantity = quantity,
                    BasePricePerKg = weightRate
                };
            }
        }

        public async Task<decimal> GetDistanceAsync(string origin, string destination)
        {
            try
            {
                string key = $"{origin}-{destination}";
                
                // Check if distance is in the cache
                if (DistanceCache.ContainsKey(key))
                {
                    return DistanceCache[key];
                }
                
                // Check if reverse direction is in the cache
                string reverseKey = $"{destination}-{origin}";
                if (DistanceCache.ContainsKey(reverseKey))
                {
                    return DistanceCache[reverseKey];
                }
                
                // In a real application, you might calculate the distance here or call an external API
                // For now, return a default value
                return 1000m;
            }
            catch
            {
                // Default distance for error cases
                return 1000m;
            }
        }

        // Temporary data for development and when database is empty
        private List<RouteInfo> GetTemporaryRouteData()
        {
            return new List<RouteInfo>
            {
                new RouteInfo 
                {
                    Origin = "Shenzhen",
                    Destination = "Sao Paulo",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 250m},
                        {"Sea", 180m}
                    },
                    BulkThreshold = 10,
                    BulkDiscountRate = 0.10m,
                    CustomServiceCharge = 50m,
                    InsuranceCharge = 20m,
                    Distance = 17500m
                },
                new RouteInfo 
                {
                    Origin = "Shenzhen",
                    Destination = "Ushuaia",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 300m},
                        {"Sea", 220m}
                    },
                    BulkThreshold = 8,
                    BulkDiscountRate = 0.12m,
                    CustomServiceCharge = 60m,
                    InsuranceCharge = 25m,
                    Distance = 19000m
                },
                new RouteInfo 
                {
                    Origin = "Shenzhen",
                    Destination = "Toronto",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 290m},
                        {"Sea", 210m}
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m,
                    CustomServiceCharge = 55m,
                    InsuranceCharge = 30m,
                    Distance = 12000m
                },
                new RouteInfo 
                {
                    Origin = "Shenzhen",
                    Destination = "Vancouver",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 275m},
                        {"Sea", 195m}
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m,
                    CustomServiceCharge = 55m,
                    InsuranceCharge = 30m,
                    Distance = 10500m
                },
                new RouteInfo 
                {
                    Origin = "Shenzhen",
                    Destination = "Mexico City",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 260m},
                        {"Sea", 210m}
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m,
                    CustomServiceCharge = 30m,
                    InsuranceCharge = 20m,
                    Distance = 14000m
                },
                new RouteInfo 
                {
                    Origin = "Shenzhen",
                    Destination = "Lima",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 270m},
                        {"Sea", 220m}
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m,
                    CustomServiceCharge = 30m,
                    InsuranceCharge = 20m,
                    Distance = 16000m
                },
                // Add standard country-to-country routes
                new RouteInfo 
                {
                    Origin = "USA",
                    Destination = "Canada",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 150m},
                        {"Sea", 120m}
                    },
                    BulkThreshold = 10,
                    BulkDiscountRate = 0.10m,
                    CustomServiceCharge = 40m,
                    InsuranceCharge = 15m,
                    Distance = 500m
                },
                new RouteInfo 
                {
                    Origin = "USA",
                    Destination = "Brazil",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 220m},
                        {"Sea", 160m}
                    },
                    BulkThreshold = 8,
                    BulkDiscountRate = 0.12m,
                    CustomServiceCharge = 45m,
                    InsuranceCharge = 18m,
                    Distance = 7500m
                },
                new RouteInfo 
                {
                    Origin = "China",
                    Destination = "Canada",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 280m},
                        {"Sea", 200m}
                    },
                    BulkThreshold = 10,
                    BulkDiscountRate = 0.10m,
                    CustomServiceCharge = 50m,
                    InsuranceCharge = 25m,
                    Distance = 12000m
                },
                new RouteInfo 
                {
                    Origin = "Brazil",
                    Destination = "China",
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 310m},
                        {"Sea", 240m}
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m,
                    CustomServiceCharge = 55m,
                    InsuranceCharge = 30m,
                    Distance = 18000m
                }
            };
        }
    }
}