using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Models.Domain;

namespace L_Connect.Models.ViewModels.Shipments
{
    public class ShipmentSearchViewModel
    {
        public string TrackingNumber { get; set; }
        public string ClientName { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        
        // For pagination
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
        
        // Search results
        public List<Shipment> Shipments { get; set; } = new List<Shipment>();
        }
}