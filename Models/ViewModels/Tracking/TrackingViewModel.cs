using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L_Connect.Models.ViewModels.Tracking
{
    // For the tracking form input
    public class TrackingViewModel
    {
        [Required(ErrorMessage = "Please enter a tracking number")]
        public string TrackingNumber { get; set; }
    }
}