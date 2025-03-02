using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.ViewModels.Tracking
{
    // For displaying tracking results
    // View model for displaying shipment tracking results to users.
    // This class contains all information needed to render the tracking result page,
    // including basic shipment details and formatted display properties.
    public class TrackingResultViewModel
    {
        public string TrackingNumber { get; set; }
        public string CurrentStatus { get; set; }
        public string OriginAddress { get; set; }
        public string DestinationAddress { get; set; }
        public string CurrentLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? EstimatedDeliveryDate { get; set; }
        
        // Properties not in domain model but useful for view
        public string FormattedCreationDate => CreatedAt.ToString("MMMM d, yyyy");
        public string FormattedEstimatedDelivery => EstimatedDeliveryDate?.ToString("MMMM d, yyyy") ?? "Not available";
        public bool IsDelivered => CurrentStatus == "Delivered";
        
        // For authenticated clients only
        // Flag indicating whether the user is an authenticated client who can access shipment documents.
        // This is the only difference between registered and non-registered users.
        public bool CanAccessDocuments { get; set; }
        // Complete history of status updates for the shipment.
        // Visible to all users, both registered and non-registered.
        public List<StatusUpdateInfo> StatusHistory { get; set; }

        // Nested class representing an individual status update in the shipment's history.
        // Contains details about each status change, including location and timestamp.
        public class StatusUpdateInfo
        {
            public string Status { get; set; }
            public string Location { get; set; }
            public string Notes { get; set; }
            public DateTime UpdatedAt { get; set; }
            public string FormattedDate => UpdatedAt.ToString("MMM d, yyyy h:mm tt");
        }
    }
}