using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Models.Domain;

namespace L_Connect.Models.ViewModels.Client
{
    public class ClientDashboardViewModel
    {
        public List<Shipment> ActiveShipments { get; set; }
        public List<Shipment> CompletedShipments { get; set; }
        public List<Shipment> PendingShipments { get; set; }

        public ClientDashboardViewModel()
        {
            ActiveShipments = new List<Shipment>();
            CompletedShipments = new List<Shipment>();
            PendingShipments = new List<Shipment>();
        }
    }
}