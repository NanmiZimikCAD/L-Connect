using Microsoft.AspNetCore.Mvc;
using L_Connect.Models.ViewModels.Services;
using System.Collections.Generic;

namespace L_Connect.Controllers.Services
{
    public class ServicesController : Controller
    {
        public IActionResult Index()
        {
            var services = new List<ServiceViewModel>
            {
                new ServiceViewModel 
                {
                    Route = "Shenzhen to Brazil",
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
                    Route = "Shenzhen to Iguazú",
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
                    Route = "Shenzhen to Canada",
                    TransportationMethods = new List<string> { "Flight", "Sea" },
                    BasePrices = new Dictionary<string, decimal>
                    {
                        {"Flight", 280m },
                        {"Sea", 200m }
                    },
                    BulkThreshold = 12,
                    BulkDiscountRate = 0.15m, // 15% discount if over 12 parcels
                    CustomServiceCharge = 55m,
                    InsuranceCharge = 30m
                }
            };

            return View(services);
        }
    }
}
