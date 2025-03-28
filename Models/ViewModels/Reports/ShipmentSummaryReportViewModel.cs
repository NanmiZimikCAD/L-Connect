using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.ViewModels.Reports
{
    public class ShipmentSummaryReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalShipments { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal TotalRevenue { get; set; }
        public int DeliveredShipments { get; set; }
        public int InTransitShipments { get; set; }
        public int ProcessingShipments { get; set; }
        public int PendingShipments { get; set; }
        public int CancelledShipments { get; set; }
        public List<ShipmentSummaryItem> RecentShipments { get; set; } = new List<ShipmentSummaryItem>();
        
        public class ShipmentSummaryItem
        {
            public string TrackingNumber { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Origin { get; set; }
            public string Destination { get; set; }
            public decimal Weight { get; set; }
            public decimal Cost { get; set; }
        }
    }
}