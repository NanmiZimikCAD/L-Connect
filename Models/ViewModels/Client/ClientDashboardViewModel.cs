using System;
using System.Collections.Generic;
using L_Connect.Models.Domain;
using L_Connect.Models.ViewModels.Documents;

namespace L_Connect.Models.ViewModels.Client
{
    public class ClientDashboardViewModel
    {
        public string ClientName { get; set; }
        
        // Shipment statistics
        public int TotalShipments { get; set; }
        public int DeliveredShipments { get; set; }
        public int InTransitShipments { get; set; }
        public int ProcessingShipments { get; set; }
        
        // Active shipments
        public List<Shipment> ActiveShipments { get; set; }
        
        // Recently viewed items
        public List<RecentlyViewedItem> RecentlyViewed { get; set; }
        
        // Available documents
        public List<DocumentViewModel> RecentDocuments { get; set; }

        public ClientDashboardViewModel()
        {
            ActiveShipments = new List<Shipment>();
            RecentlyViewed = new List<RecentlyViewedItem>();
            RecentDocuments = new List<DocumentViewModel>();
        }
        
        // Inner class for recently viewed items
        public class RecentlyViewedItem
        {
            public string ItemType { get; set; } // "Shipment", "Quote", "Invoice"
            public string ItemId { get; set; }
            public string ItemTitle { get; set; }
            public string Status { get; set; }
            public string StatusClass { get; set; } // For the badge color class
            public DateTime ViewedDate { get; set; }
            public string AdditionalInfo { get; set; } // ETA, Amount, etc.
        }
    }
}