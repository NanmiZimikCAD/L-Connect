using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Models.ViewModels.Reports;

namespace L_Connect.Services.Interfaces
{
    public interface IReportService
    {
        // Shipment Summary report - shows basic stats for shipments in a date range
        Task<ShipmentSummaryReportViewModel> GetShipmentSummaryReportAsync(DateTime startDate, DateTime endDate);
        
        // Status Distribution report - shows counts of shipments by status
        Task<StatusDistributionReportViewModel> GetStatusDistributionReportAsync(DateTime startDate, DateTime endDate);
        
        // Client Activity report - shows shipment activity by client
        Task<ClientActivityReportViewModel> GetClientActivityReportAsync(DateTime startDate, DateTime endDate);
    }
}