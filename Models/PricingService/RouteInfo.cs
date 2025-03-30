using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.PricingService
{
    public class RouteInfo
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Dictionary<string, decimal> BasePrices { get; set; } = new Dictionary<string, decimal>();
        public int BulkThreshold { get; set; }
        public decimal BulkDiscountRate { get; set; }
        public decimal CustomServiceCharge { get; set; }
        public decimal InsuranceCharge { get; set; }
        public decimal Distance { get; set; }
    }

    public class QuoteCalculationResult
    {
        public decimal BaseCost { get; set; }
        public decimal WeightCost { get; set; }
        public decimal SubtotalBeforeDiscounts { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal CustomServiceCharge { get; set; }
        public decimal InsuranceCharge { get; set; }
        public decimal TotalCost { get; set; }
        public string TransportationMethod { get; set; }
        public decimal Distance { get; set; }
        public int Quantity { get; set; }
        public decimal BasePricePerKg { get; set; }
    }
}