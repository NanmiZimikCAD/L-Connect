using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Models.PricingService;

namespace L_Connect.Services.Interfaces
{
    public interface IPricingService
    {
        Task<List<RouteInfo>> GetAllRoutesAsync();
        Task<List<string>> GetAvailableOriginsAsync();
        Task<List<string>> GetAvailableDestinationsAsync(string origin = null);
        Task<RouteInfo> GetRoutePricingAsync(string origin, string destination);
        Task<Dictionary<string, decimal>> GetTransportationMethodRatesAsync(string origin, string destination);
        Task<QuoteCalculationResult> CalculateShippingCostAsync(
            string origin, 
            string destination, 
            decimal weight, 
            string transportMethod, 
            int quantity = 1, 
            bool needsCustomServices = false, 
            bool needsInsurance = false);
        
        // Helper method to get distance between locations
        Task<decimal> GetDistanceAsync(string origin, string destination);
    }
}