using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using L_Connect.Models.ViewModels.Quote;
using L_Connect.Services.Interfaces;

namespace L_Connect.Controllers
{
    public class QuoteRequestController : Controller
    {
        private readonly IPricingService _pricingService;

        public QuoteRequestController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                // Get all available origins
                var origins = await _pricingService.GetAvailableOriginsAsync();
                
                // Initialize the view model
                var model = new QuoteRequestViewModel
                {
                    AvailableOrigins = origins,
                    AvailableDestinations = new List<string>(), // Will be populated via AJAX when origin is selected
                    Quantity = 1
                };
                
                // Set default values for first origin if available
                if (origins.Any())
                {
                    var firstOrigin = origins.First();
                    model.Source = firstOrigin;
                    
                    // Get destinations for the first origin
                    model.AvailableDestinations = await _pricingService.GetAvailableDestinationsAsync(firstOrigin);
                    
                    // Set first destination if available
                    if (model.AvailableDestinations.Any())
                    {
                        model.Destination = model.AvailableDestinations.First();
                        
                        // Get transportation methods and prices
                        model.TransportMethodPrices = await _pricingService.GetTransportationMethodRatesAsync(
                            model.Source, model.Destination);
                        
                        if (model.TransportMethodPrices.Any())
                        {
                            model.TransportationMethod = model.TransportMethodPrices.Keys.First();
                        }
                        
                        // Get route info for additional details
                        var routeInfo = await _pricingService.GetRoutePricingAsync(model.Source, model.Destination);
                        if (routeInfo != null)
                        {
                            model.CustomServiceCharge = routeInfo.CustomServiceCharge;
                            model.InsuranceCharge = routeInfo.InsuranceCharge;
                            model.BulkThreshold = routeInfo.BulkThreshold;
                            model.BulkDiscountRate = routeInfo.BulkDiscountRate;
                        }
                    }
                }
                
                return View(model);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in Create GET: {ex.Message}");
                
                // Return a basic model with default countries
                return View(new QuoteRequestViewModel 
                {
                    AvailableOrigins = new List<string> { "USA", "Canada", "Brazil", "China" },
                    AvailableDestinations = new List<string> { "USA", "Canada", "Brazil", "China" },
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuoteRequestViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdown options
                model.AvailableOrigins = await _pricingService.GetAvailableOriginsAsync();
                model.AvailableDestinations = await _pricingService.GetAvailableDestinationsAsync(model.Source);
                model.TransportMethodPrices = await _pricingService.GetTransportationMethodRatesAsync(
                    model.Source, model.Destination);
                
                // Get route info for additional details
                var routeInfo = await _pricingService.GetRoutePricingAsync(model.Source, model.Destination);
                if (routeInfo != null)
                {
                    model.CustomServiceCharge = routeInfo.CustomServiceCharge;
                    model.InsuranceCharge = routeInfo.InsuranceCharge;
                    model.BulkThreshold = routeInfo.BulkThreshold;
                    model.BulkDiscountRate = routeInfo.BulkDiscountRate;
                }
                
                return View(model);
            }

            try
            {
                // Calculate shipping cost using the pricing service
                var result = await _pricingService.CalculateShippingCostAsync(
                    model.Source,
                    model.Destination,
                    model.Weight,
                    model.TransportationMethod,
                    model.Quantity,
                    model.NeedsCustomServices,
                    model.NeedsInsurance
                );

                // Map to response view model
                var quoteResponse = new QuoteResponseViewModel
                {
                    CustomerName = model.CustomerName,
                    Email = model.Email,
                    Source = model.Source,
                    Destination = model.Destination,
                    Weight = model.Weight,
                    Quantity = model.Quantity,
                    TransportationMethod = model.TransportationMethod,
                    BasePricePerKg = result.BasePricePerKg,
                    Distance = result.Distance,
                    BaseCost = result.BaseCost,
                    WeightCost = result.WeightCost,
                    SubtotalBeforeDiscounts = result.SubtotalBeforeDiscounts,
                    DiscountPercentage = result.DiscountPercentage > 0 ? result.DiscountPercentage : null,
                    DiscountAmount = result.DiscountAmount,
                    HasCustomServices = model.NeedsCustomServices,
                    CustomServiceCharge = result.CustomServiceCharge,
                    HasInsurance = model.NeedsInsurance,
                    InsuranceCharge = result.InsuranceCharge,
                    EstimatedPrice = result.TotalCost
                };

                return View("QuoteResponse", quoteResponse);
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error calculating quote: {ex.Message}");
                
                // Add error to ModelState
                ModelState.AddModelError("", "An error occurred while calculating your quote. Please try again.");
                
                // Repopulate dropdown options
                model.AvailableOrigins = await _pricingService.GetAvailableOriginsAsync();
                model.AvailableDestinations = await _pricingService.GetAvailableDestinationsAsync(model.Source);
                
                return View(model);
            }
        }

        // AJAX endpoints for dynamic form updates
        [HttpGet]
        public async Task<JsonResult> GetDestinations(string origin)
        {
            if (string.IsNullOrEmpty(origin))
            {
                return Json(new List<string>());
            }

            var destinations = await _pricingService.GetAvailableDestinationsAsync(origin);
            return Json(destinations);
        }

        [HttpGet]
        public async Task<JsonResult> GetTransportMethods(string origin, string destination)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            {
                return Json(new Dictionary<string, decimal>());
            }

            var transportMethods = await _pricingService.GetTransportationMethodRatesAsync(origin, destination);
            return Json(transportMethods);
        }

        [HttpGet]
        public async Task<JsonResult> GetRouteInfo(string origin, string destination)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            {
                return Json(null);
            }

            var routeInfo = await _pricingService.GetRoutePricingAsync(origin, destination);
            
            // Create a simplified object with just the needed properties
            var result = new
            {
                CustomServiceCharge = routeInfo?.CustomServiceCharge ?? 50m,
                InsuranceCharge = routeInfo?.InsuranceCharge ?? 20m,
                BulkThreshold = routeInfo?.BulkThreshold ?? 10,
                BulkDiscountRate = routeInfo?.BulkDiscountRate ?? 0.1m
            };
            
            return Json(result);
        }
    }
}