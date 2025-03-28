using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Data;
using L_Connect.Models.ViewModels.Reports;
using L_Connect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L_Connect.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;
        
        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<ShipmentSummaryReportViewModel> GetShipmentSummaryReportAsync(DateTime startDate, DateTime endDate)
        {
            // Query shipments within the date range
            var shipments = await _context.Shipments
                .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
                .Include(s => s.Client)
                .ToListAsync();
            
            var model = new ShipmentSummaryReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalShipments = shipments.Count,
                TotalWeight = shipments.Sum(s => s.Weight),
                TotalRevenue = shipments.Sum(s => s.FinalCost),
                DeliveredShipments = shipments.Count(s => s.CurrentStatus == "Delivered"),
                InTransitShipments = shipments.Count(s => s.CurrentStatus == "In Transit"),
                ProcessingShipments = shipments.Count(s => s.CurrentStatus == "Processing"),
                PendingShipments = shipments.Count(s => s.CurrentStatus == "Pending"),
                CancelledShipments = shipments.Count(s => s.CurrentStatus == "Cancelled"),
                RecentShipments = shipments
                    .OrderByDescending(s => s.CreatedAt)
                    .Take(10)
                    .Select(s => new ShipmentSummaryReportViewModel.ShipmentSummaryItem
                    {
                        TrackingNumber = s.TrackingNumber,
                        Status = s.CurrentStatus,
                        CreatedAt = s.CreatedAt,
                        Origin = s.OriginAddress,
                        Destination = s.DestinationAddress,
                        Weight = s.Weight,
                        Cost = s.FinalCost
                    })
                    .ToList()
            };
            
            return model;
        }
        
        public async Task<StatusDistributionReportViewModel> GetStatusDistributionReportAsync(DateTime startDate, DateTime endDate)
        {
            // Query shipments within the date range
            var shipments = await _context.Shipments
                .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
                .ToListAsync();
            
            // Group by status
            var statusGroups = shipments
                .GroupBy(s => s.CurrentStatus)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();
            
            var model = new StatusDistributionReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalShipments = shipments.Count,
                StatusCounts = statusGroups.ToDictionary(g => g.Status, g => g.Count)
            };
            
            // Create status items with percentages
            model.StatusItems = statusGroups
                .Select(g => new StatusDistributionReportViewModel.StatusDistributionItem
                {
                    Status = g.Status,
                    Count = g.Count,
                    Percentage = model.TotalShipments > 0 
                        ? Math.Round((decimal)g.Count / model.TotalShipments * 100, 2) 
                        : 0
                })
                .OrderByDescending(i => i.Count)
                .ToList();
            
            return model;
        }
        
        public async Task<ClientActivityReportViewModel> GetClientActivityReportAsync(DateTime startDate, DateTime endDate)
        {
            // Query shipments within the date range
            var shipments = await _context.Shipments
                .Where(s => s.CreatedAt >= startDate && s.CreatedAt <= endDate)
                .Include(s => s.Client)
                .ToListAsync();
            
            // Group by client
            var clientGroups = shipments
                .Where(s => s.ClientId.HasValue)
                .GroupBy(s => s.Client)
                .Select(g => new ClientActivityReportViewModel.ClientActivityItem
                {
                    ClientId = g.Key.UserId,
                    ClientName = g.Key.FullName,
                    ClientEmail = g.Key.Email,
                    ShipmentCount = g.Count(),
                    TotalWeight = g.Sum(s => s.Weight),
                    TotalSpent = g.Sum(s => s.FinalCost),
                    LastActivity = g.Max(s => s.CreatedAt)
                })
                .OrderByDescending(c => c.ShipmentCount)
                .ToList();
            
            var model = new ClientActivityReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalClients = clientGroups.Count,
                TotalShipments = shipments.Count(s => s.ClientId.HasValue),
                ClientActivities = clientGroups
            };
            
            return model;
        }
    }
}