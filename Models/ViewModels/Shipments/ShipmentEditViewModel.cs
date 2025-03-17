using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace L_Connect.Models.ViewModels.Shipments
{
    public class ShipmentEditViewModel
    {
        public int ShipmentId { get; set; }
    
        [Display(Name = "Tracking Number")]
        public string TrackingNumber { get; set; }
        
        [Display(Name = "Client")]
        public string ClientName { get; set; }
        
        [Required]
        [Display(Name = "Weight (kg)")]
        [Range(0.1, 10000)]
        public decimal Weight { get; set; }
        
        [Required]
        [Display(Name = "Origin Address")]
        [StringLength(255)]
        public string OriginAddress { get; set; }
        
        [Required]
        [Display(Name = "Destination Address")]
        [StringLength(255)]
        public required string DestinationAddress { get; set; }
        
        [Required]
        [Display(Name = "Current Status")]
        public string CurrentStatus { get; set; }
        
        [Required]
        [Display(Name = "Current Location")]
        public string CurrentLocation { get; set; }
        
        [Required]
        [Display(Name = "Final Cost")]
        [Range(0, 1000000)]
        public decimal FinalCost { get; set; }
        
        [Required]
        [Display(Name = "Estimated Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime? EstimatedDeliveryDate { get; set; }
        
        [Required]
        [Display(Name = "Service Type")]
        public string ServiceType { get; set; }
        
        // Add dropdown options for Status
        public List<SelectListItem> StatusOptions { get; set; }
        
        // Add dropdown options for Service Type
        public List<SelectListItem> ServiceTypeOptions { get; set; }
    }
}