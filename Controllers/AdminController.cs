using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using L_Connect.Services.Interfaces;
using L_Connect.Services.Implementations;
using L_Connect.Models.ViewModels.Shipments;

// AdminController manages administrative functionalities including shipment creation, user management and reporting

namespace L_Connect.Controllers
{
    public class AdminController : Controller
    {
        private readonly IShipmentService _shipmentService;
        private readonly IUserService _userService;

        public AdminController(IShipmentService shipmentService, IUserService userService)
        {
            _shipmentService = shipmentService;
            _userService = userService;
        }

        // Existing Dashboard method remains unchanged
        public IActionResult Dashboard()
        {
            return View();
        }

        // Add these new methods:
        
        // GET: Admin/CreateShipment
        public IActionResult CreateShipment()
        {
            ViewBag.ServiceTypes = new List<string> { "Standard", "Express", "International" };
            return View(new CreateShipmentViewModel());
        }

        // POST: Admin/CreateShipment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateShipment(CreateShipmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ServiceTypes = new List<string> { "Standard", "Express", "International" };
                return View(model);
            }
            try
            {
                // Check if the client exists
                var clientExists = await _userService.DoesUserExistByEmail(model.ClientEmail);
                int? clientId = null;

                if (clientExists)
                {
                    // Get the client ID
                    var client = await _userService.GetUserByEmail(model.ClientEmail);
                    clientId = client.UserId; // Assuming your User model has UserId property
                }

                int createdByAdminId = 1;

                // Try to get admin ID from claims if available
                var userIdClaim = User.FindFirst("UserId");
                if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
                {
                    createdByAdminId = int.Parse(userIdClaim.Value);
                }

                // Create the shipment - make sure to import L_Connect.Models.Domain namespace
                var shipment = new L_Connect.Models.Domain.Shipment
                {
                    ClientId = clientId,
                    OriginAddress = model.OriginAddress,
                    DestinationAddress = model.DestinationAddress,
                    Weight = model.Weight,
                    CurrentStatus = "Pending",
                    ServiceType = model.ServiceType, // Now directly setting the ServiceType
                    //CreatedByAdminId = int.Parse(User.FindFirst("UserId")?.Value),
                    CreatedByAdminId = createdByAdminId,
                    CreatedAt = DateTime.UtcNow,
                    // Set any other required properties:
                    FinalCost = CalculateShippingCost(model.Weight, model.ServiceType)
                };

                var result = await _shipmentService.CreateShipmentAsync(shipment);

                TempData["SuccessMessage"] = $"Shipment created successfully. Tracking Number: {result.TrackingNumber}";
                return RedirectToAction("ShipmentDetails", new { id = result.ShipmentId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating shipment: {ex.Message}");
                ViewBag.ServiceTypes = new List<string> { "Standard", "Express", "International" };
                return View(model);
            }
        }

        // GET: Admin/ShipmentDetails/{id}
        public async Task<IActionResult> ShipmentDetails(int id)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }

            // Get status history separately
            var statusHistory = await _shipmentService.GetShipmentStatusHistoryAsync(id);
            
            // Use a view model to combine shipment and status history
            var viewModel = new ShipmentDetailsViewModel
            {
                Shipment = shipment,
                StatusHistory = statusHistory
            };

            return View(viewModel);
        }

        // Helper method to calculate shipping cost (this could be moved to a service)
        private decimal CalculateShippingCost(decimal weight, string serviceType)
        {
            decimal baseCost = 10.00m;
            decimal perKgRate = 2.50m;
            
            switch (serviceType)
            {
                case "Express":
                    perKgRate = 4.75m;
                    break;
                case "International":
                    baseCost = 25.00m;
                    perKgRate = 8.50m;
                    break;
            }
            
            return baseCost + (weight * perKgRate);
        }
    }
}