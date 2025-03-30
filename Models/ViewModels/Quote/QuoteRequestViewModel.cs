using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace L_Connect.Models.ViewModels.Quote
{
    public class QuoteRequestViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Customer name is required")]
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Source is required")]
        [Display(Name = "Source Location")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Display(Name = "Weight (kg)")]
        [Range(0.1, 10000, ErrorMessage = "Weight must be between 0.1 and 10,000 kg")]
        public decimal Weight { get; set; }
        
        [Required(ErrorMessage = "Transportation method is required")]
        [Display(Name = "Transportation Method")]
        public string TransportationMethod { get; set; }
        
        [Display(Name = "Quantity")]
        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1,000")]
        public int Quantity { get; set; } = 1;
        
        [Display(Name = "Custom Service")]
        public bool NeedsCustomServices { get; set; }
        
        [Display(Name = "Insurance")]
        public bool NeedsInsurance { get; set; }

        [Display(Name = "Additional Details")]
        public string AdditionalDetails { get; set; }

        // Options for dropdowns and selection
        public List<string> AvailableOrigins { get; set; } = new List<string>();
        public List<string> AvailableDestinations { get; set; } = new List<string>();
        public Dictionary<string, decimal> TransportMethodPrices { get; set; } = new Dictionary<string, decimal>();
        
        // Information for UI display
        public decimal CustomServiceCharge { get; set; }
        public decimal InsuranceCharge { get; set; }
        public int BulkThreshold { get; set; }
        public decimal BulkDiscountRate { get; set; }

        // Custom validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Weight <= 0)
            {
                yield return new ValidationResult("Weight must be greater than 0", new[] { nameof(Weight) });
            }

            if (Quantity <= 0)
            {
                yield return new ValidationResult("Quantity must be at least 1", new[] { nameof(Quantity) });
            }

            if (Source == Destination)
            {
                yield return new ValidationResult("Source and destination cannot be the same", new[] { nameof(Destination) });
            }
        }
    }
}