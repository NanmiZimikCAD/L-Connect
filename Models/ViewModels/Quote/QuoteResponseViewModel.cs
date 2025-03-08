namespace L_Connect.Models.ViewModels.Quote
{
    public class QuoteResponseViewModel
    {
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        
        // Format to 2 decimal places
        public decimal Weight { get; set; }
        public decimal BasePricePerKg { get; set; }
        public decimal Distance { get; set; }
        public decimal EstimatedPrice { get; set; }
        public decimal? Discount { get; set; }
    }
}
