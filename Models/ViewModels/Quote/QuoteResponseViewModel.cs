namespace L_Connect.Models.ViewModels.Quote
{
    public class QuoteResponseViewModel
    {
        // Customer information
        public string CustomerName { get; set; }
        public string Email { get; set; }
        
        // Shipment details
        public string Source { get; set; }
        public string Destination { get; set; }
        public decimal Weight { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal TotalWeight => Weight * Quantity;
        
        // Transportation details
        public string TransportationMethod { get; set; }
        public decimal BasePricePerKg { get; set; }
        public decimal Distance { get; set; }
        
        // Cost breakdown
        public decimal BaseCost { get; set; }
        public decimal WeightCost { get; set; }
        public decimal SubtotalBeforeDiscounts { get; set; }
        
        // Discounts
        public decimal? DiscountPercentage { get; set; }
        public decimal DiscountAmount { get; set; }
        
        // Additional services
        public bool HasCustomServices { get; set; }
        public decimal CustomServiceCharge { get; set; }
        public bool HasInsurance { get; set; }
        public decimal InsuranceCharge { get; set; }
        
        // Final cost
        public decimal EstimatedPrice { get; set; }
        
        // Formatting helpers
        public string FormattedWeight => Weight.ToString("N2");
        public string FormattedTotalWeight => TotalWeight.ToString("N2");
        public string FormattedDistance => Distance.ToString("N2");
        public string FormattedBaseCost => string.Format("${0:0.00}", BaseCost);
        public string FormattedWeightCost => string.Format("${0:0.00}", WeightCost);
        public string FormattedSubtotal => string.Format("${0:0.00}", SubtotalBeforeDiscounts);
        public string FormattedDiscountAmount => string.Format("${0:0.00}", DiscountAmount);
        public string FormattedCustomServiceCharge => string.Format("${0:0.00}", CustomServiceCharge);
        public string FormattedInsuranceCharge => string.Format("${0:0.00}", InsuranceCharge);
        public string FormattedEstimatedPrice => string.Format("${0:0.00}", EstimatedPrice);
    }
}