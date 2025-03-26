using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Models.Domain;
using L_Connect.Models.ViewModels.Documents;

namespace L_Connect.Models.ViewModels.Shipments
{
    public class ShipmentDetailsViewModel
    {
        public Shipment Shipment { get; set; }
        public List<ShipmentStatus> StatusHistory { get; set; }

         // Helper properties to extract information not directly in the Shipment model
        public string ClientEmail => Shipment.Client?.Email ?? "No email associated";
        
        // ServiceType isn't in our model, so we get calculated value based on other properties
        public string ServiceType
        {
            get
            {
                if (Shipment.FinalCost > 50)
                    return "International";
                else if (Shipment.FinalCost > 25)
                    return "Express";
                else
                    return "Standard";
            }
        }

        // Add this new property
        public IEnumerable<DocumentViewModel> Documents { get; set; }
    }
}