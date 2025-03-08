using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace L_Connect.Models.ViewModels.Quote
{
    public class QuoteRequestViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Source is required")]
        public string Source { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public decimal Weight { get; set; }

        public string AdditionalDetails { get; set; }

        public List<string> Countries { get; set; } = new List<string>
        {
            "USA",
            "Canada",
            "Brazil",
            "China"
        };

        // Custom Validation for decimal range
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Weight < 1 || Weight > 100000) // Adjust max value based on realistic limits
            {
                yield return new ValidationResult("Weight must be between 1 and 100,000 kg", new[] { nameof(Weight) });
            }
        }
    }
}
