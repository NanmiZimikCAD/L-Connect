using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace L_Connect.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;
        
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        
        // GET: Report/Index
        [HttpGet]
        [Route("Report/Index")]
        public IActionResult Index()
        {
            // Set default date range (last 30 days)
            ViewBag.StartDate = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            
            return View();
        }
        
        // GET: Report/ShipmentSummary
        [HttpGet]
        [Route("Report/ShipmentSummary")]
        public async Task<IActionResult> ShipmentSummary(DateTime? startDate, DateTime? endDate)
        {
            // Default to last 30 days if dates not provided
            var start = startDate ?? DateTime.Now.AddDays(-30);
            var end = endDate ?? DateTime.Now;
            
            var report = await _reportService.GetShipmentSummaryReportAsync(start, end);
            return View(report);
        }
        
        // GET: Report/StatusDistribution
        [HttpGet]
        [Route("Report/StatusDistribution")]
        public async Task<IActionResult> StatusDistribution(DateTime? startDate, DateTime? endDate)
        {
            // Default to last 30 days if dates not provided
            var start = startDate ?? DateTime.Now.AddDays(-30);
            var end = endDate ?? DateTime.Now;
            
            var report = await _reportService.GetStatusDistributionReportAsync(start, end);
            return View(report);
        }
        
        // GET: Report/ClientActivity
        [HttpGet]
        [Route("Report/ClientActivity")]
        public async Task<IActionResult> ClientActivity(DateTime? startDate, DateTime? endDate)
        {
            // Default to last 30 days if dates not provided
            var start = startDate ?? DateTime.Now.AddDays(-30);
            var end = endDate ?? DateTime.Now;
            
            var report = await _reportService.GetClientActivityReportAsync(start, end);
            return View(report);
        }
    }
}