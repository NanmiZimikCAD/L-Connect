using Microsoft.AspNetCore.Mvc;
using L_Connect.Models.ViewModels.Services;
using L_Connect.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IPricingService _pricingService;

        public ServicesController(IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Get route information from the pricing service
                var routes = await _pricingService.GetAllRoutesAsync();
                
                // Convert to ServiceViewModel list
                var services = routes.Select(r => new ServiceViewModel
                {
                    Route = $"{r.Origin} to {r.Destination}",
                    TransportationMethods = r.BasePrices.Keys.ToList(),
                    BasePrices = r.BasePrices,
                    BulkThreshold = r.BulkThreshold,
                    BulkDiscountRate = r.BulkDiscountRate,
                    CustomServiceCharge = r.CustomServiceCharge,
                    InsuranceCharge = r.InsuranceCharge
                }).ToList();

                return View(services);
            }
            catch
            {
                // If there's an error, fall back to hardcoded data
                return View(GetLegacyServiceData());
            }
        }

        // Fallback method to maintain backwards compatibility
        private List<ServiceViewModel> GetLegacyServiceData()
        {
            return new List<ServiceViewModel>
            {
                new ServiceViewModel 
                {
                    Route = "Shenzhen to Sao Paulo",
                    TransportationMethods = new List<string> { "Flight", "Sea" },
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 250m },
                        {"Sea", 180m }
                    },
                    BulkThreshold = 10,
                    BulkDiscountRate = 0.10m, // 10% discount if over 10 parcels
                    CustomServiceCharge = 50m,
                    InsuranceCharge = 20m
                },
                new ServiceViewModel 
                {
                    Route = "Shenzhen to Ushuaia",
                    TransportationMethods = new List<string> { "Flight", "Sea" },
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 300m },
                        {"Sea", 220m }
                    },
                    BulkThreshold = 8,
                    BulkDiscountRate = 0.12m, // 12% discount if over 8 parcels
                    CustomServiceCharge = 60m,
                    InsuranceCharge = 25m
                },
                new ServiceViewModel 
                {
                    Route = "Shenzhen to Toronto",
                    TransportationMethods = new List<string> { "Flight", "Sea" },
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 290m },
                        {"Sea", 210m }
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m, // 15% discount if over 12 parcels
                    CustomServiceCharge = 55m,
                    InsuranceCharge = 30m
                },
                new ServiceViewModel 
                {
                    Route = "Shenzhen to Vancouver",
                    TransportationMethods = new List<string> { "Flight", "Sea" },
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 275m },
                        {"Sea", 195m }
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m, // 15% discount if over 12 parcels
                    CustomServiceCharge = 55m,
                    InsuranceCharge = 30m
                },
                new ServiceViewModel 
                {
                    Route = "Shenzhen to Mexico City",
                    TransportationMethods = new List<string> { "Flight", "Sea" },
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 260m },
                        {"Sea", 210m }
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m, // 15% discount if over 12 parcels
                    CustomServiceCharge = 30m,
                    InsuranceCharge = 20m
                },
                new ServiceViewModel 
                {
                    Route = "Shenzhen to Lima",
                    TransportationMethods = new List<string> { "Flight", "Sea" },
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 270m },
                        {"Sea", 220m }
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m, // 15% discount if over 12 parcels
                    CustomServiceCharge = 30m,
                    InsuranceCharge = 20m
                }
            };
        }
    }
}