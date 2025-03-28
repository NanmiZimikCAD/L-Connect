using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.ViewModels.Reports
{
    public class ClientActivityReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalClients { get; set; }
        public int TotalShipments { get; set; }
        public List<ClientActivityItem> ClientActivities { get; set; } = new List<ClientActivityItem>();
        
        public class ClientActivityItem
        {
            public int ClientId { get; set; }
            public string ClientName { get; set; }
            public string ClientEmail { get; set; }
            public int ShipmentCount { get; set; }
            public decimal TotalWeight { get; set; }
            public decimal TotalSpent { get; set; }
            public DateTime LastActivity { get; set; }
        }
    }
}