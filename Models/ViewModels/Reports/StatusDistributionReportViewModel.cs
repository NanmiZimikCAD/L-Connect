using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.ViewModels.Reports
{
    public class StatusDistributionReportViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Dictionary<string, int> StatusCounts { get; set; } = new Dictionary<string, int>();
        public int TotalShipments { get; set; }
        
        // For displaying in a table
        public List<StatusDistributionItem> StatusItems { get; set; } = new List<StatusDistributionItem>();
        
        public class StatusDistributionItem
        {
            public string Status { get; set; }
            public int Count { get; set; }
            public decimal Percentage { get; set; }
        }
    }
}