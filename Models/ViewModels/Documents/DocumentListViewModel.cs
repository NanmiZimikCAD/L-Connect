using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.ViewModels.Documents
{
    public class DocumentListViewModel
    {
        public int ShipmentID { get; set; }
        public string ShipmentTrackingNumber { get; set; }
        public IEnumerable<DocumentViewModel> Documents { get; set; }
    }
}