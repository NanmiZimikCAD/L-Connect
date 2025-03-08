using System.Collections.Generic;

namespace L_Connect.Models.ViewModels.Services
{
    public class ServiceViewModel
    {
        public string Route { get; set; }
        public List<string> TransportationMethods { get; set; }
        public Dictionary<string, decimal> BasePrices { get; set; }  // e.g. "Flight": 250, "Sea": 180
        public int BulkThreshold { get; set; }                       // e.g. 10 parcels
        public decimal BulkDiscountRate { get; set; }                // e.g. 0.1 (10% discount)
        public decimal CustomServiceCharge { get; set; }             // extra charge per parcel
        public decimal InsuranceCharge { get; set; }                 // extra charge per parcel
    }
}
