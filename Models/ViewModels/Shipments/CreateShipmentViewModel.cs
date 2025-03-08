using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace L_Connect.Models.ViewModels.Shipments
{
    public class CreateShipmentViewModel
    {
        [Required(ErrorMessage = "Client email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [Display(Name = "Client Email")]
        public string ClientEmail { get; set; }

        [Required(ErrorMessage = "Origin address is required")]
        [Display(Name = "Origin Address")]
        public string OriginAddress { get; set; }

        [Required(ErrorMessage = "Destination address is required")]
        [Display(Name = "Destination Address")]
        public string DestinationAddress { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0.1, 1000, ErrorMessage = "Weight must be between 0.1 and 1000 kg")]
        [Display(Name = "Weight (kg)")]
        public decimal Weight { get; set; }

        [Display(Name = "Description (Optional)")]
        public string Description { get; set; }

        [Display(Name = "Special Instructions (Optional)")]
        public string SpecialInstructions { get; set; }

        [Display(Name = "Service Type")]
        [Required(ErrorMessage = "Service type is required")]
        public string ServiceType { get; set; }
    }
}