using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using L_Connect.Services.Interfaces;
using L_Connect.Models.ViewModels.Shipments;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using L_Connect.Models.Domain;

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

        // GET: Admin/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var shipments = await _shipmentService.GetAllShipmentsAsync();
            return View(shipments);
        }

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
                    clientId = client.UserId;
                }

                int createdByAdminId = GetCurrentAdminId();

                // Create the shipment
                var shipment = new Shipment
                {
                    ClientId = clientId,
                    OriginAddress = model.OriginAddress,
                    DestinationAddress = model.DestinationAddress,
                    Weight = model.Weight,
                    CurrentStatus = "Pending",
                    ServiceType = model.ServiceType,
                    CreatedByAdminId = createdByAdminId,
                    CreatedAt = DateTime.UtcNow,
                    FinalCost = CalculateShippingCost(model.Weight, model.ServiceType)
                };

                var result = await _shipmentService.CreateShipmentAsync(shipment);

                TempData["SuccessMessage"] = $"Shipment created successfully. Tracking Number: {result.TrackingNumber}";
                return RedirectToAction(nameof(ShipmentDetails), new { id = result.ShipmentId });
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

        // GET: Admin/EditShipment/{id}
        [HttpGet]
        public async Task<IActionResult> EditShipment(int id)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            
            if (shipment == null)
                return NotFound();
            
            var viewModel = new ShipmentEditViewModel
            {
                ShipmentId = shipment.ShipmentId,
                TrackingNumber = shipment.TrackingNumber,
                ClientName = shipment.Client?.FullName ?? "No Client",
                Weight = shipment.Weight,
                OriginAddress = shipment.OriginAddress,
                DestinationAddress = shipment.DestinationAddress,
                CurrentStatus = shipment.CurrentStatus,
                CurrentLocation = shipment.CurrentLocation,
                FinalCost = shipment.FinalCost,
                EstimatedDeliveryDate = shipment.EstimatedDeliveryDate,
                ServiceType = shipment.ServiceType,
                
                // Populate dropdown options
                StatusOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Pending", Text = "Pending" },
                    new SelectListItem { Value = "Processing", Text = "Processing" },
                    new SelectListItem { Value = "In Transit", Text = "In Transit" },
                    new SelectListItem { Value = "Out for Delivery", Text = "Out for Delivery" },
                    new SelectListItem { Value = "Delivered", Text = "Delivered" },
                    new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
                },
                
                ServiceTypeOptions = new List<SelectListItem>
                {
                    new SelectListItem { Value = "Standard", Text = "Standard" },
                    new SelectListItem { Value = "Express", Text = "Express" },
                    new SelectListItem { Value = "Same-Day", Text = "Same-Day" },
                    new SelectListItem { Value = "International", Text = "International" }
                }
            };
            
            return View(viewModel);
        }

        // POST: Admin/EditShipment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("UpdateShipment")]
        public async Task<IActionResult> EditShipment(int ShipmentId, decimal Weight, string OriginAddress, 
            string DestinationAddress, string CurrentStatus, string CurrentLocation, 
            DateTime? EstimatedDeliveryDate, decimal FinalCost, string ServiceType)
        {
            System.Diagnostics.Debug.WriteLine($"EditShipment POST called with ID: {ShipmentId}");
            
            // Get existing shipment
            var shipment = await _shipmentService.GetShipmentByIdAsync(ShipmentId);
            
            if (shipment == null)
                return NotFound();
                
            // Update fields
            shipment.Weight = Weight;
            shipment.OriginAddress = OriginAddress;
            shipment.DestinationAddress = DestinationAddress;
            shipment.CurrentStatus = CurrentStatus;
            shipment.CurrentLocation = CurrentLocation;
            shipment.EstimatedDeliveryDate = EstimatedDeliveryDate;
            shipment.FinalCost = FinalCost;
            shipment.ServiceType = ServiceType;
            
            // Update
            var result = await _shipmentService.UpdateShipmentAsync(shipment, GetCurrentAdminId());
            
            if (result)
            {
                TempData["SuccessMessage"] = "Shipment updated successfully.";
                return RedirectToAction(nameof(Dashboard));
            }
            
            TempData["ErrorMessage"] = "Failed to update shipment.";
            return RedirectToAction(nameof(EditShipment), new { id = ShipmentId });
        }

        // GET: Admin/DeleteShipment/{id}
        public async Task<IActionResult> DeleteShipment(int id)
        {
            // Get the shipment
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            
            // Validate shipment exists
            if (shipment == null)
            {
                return NotFound();
            }
            
            // Check if status allows deletion
            if (shipment.CurrentStatus != "Pending" && shipment.CurrentStatus != "Cancelled")
            {
                TempData["ErrorMessage"] = "Only shipments with Pending or Cancelled status can be deleted.";
                return RedirectToAction(nameof(Dashboard));
            }
            
            // Call service method to delete
            var result = await _shipmentService.DeleteShipmentAsync(id);
            
            if (result)
            {
                TempData["SuccessMessage"] = "Shipment deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete shipment.";
            }
            
            return RedirectToAction(nameof(Dashboard));
        }

        // GET: Admin/TestUpdate/{id}
        [HttpGet]
        public IActionResult TestUpdate(int id)
        {
            return View(id);
        }

        // POST: Admin/TestUpdate
        [HttpPost]
        public async Task<IActionResult> TestUpdate(int id, string status)
        {
            System.Diagnostics.Debug.WriteLine($"TestUpdate POST received. ID: {id}, Status: {status}");
            
            var shipment = await _shipmentService.GetShipmentByIdAsync(id);
            
            if (shipment == null)
                return NotFound();
            
            // Update just the status
            shipment.CurrentStatus = status;
            
            var result = await _shipmentService.UpdateShipmentAsync(shipment, GetCurrentAdminId(), "Test update");
            
            if (result)
            {
                TempData["SuccessMessage"] = "Test update successful!";
            }
            else
            {
                TempData["ErrorMessage"] = "Test update failed!";
            }
            
            return RedirectToAction(nameof(Dashboard));
        }

        // Helper method to calculate shipping cost
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
        
        // Helper method to get the current admin's ID
        private int GetCurrentAdminId()
        {
            // Get the current user ID from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            
            // Try to get admin ID from claims if available
            var customUserIdClaim = User.FindFirst("UserId");
            if (customUserIdClaim != null && !string.IsNullOrEmpty(customUserIdClaim.Value))
            {
                return int.Parse(customUserIdClaim.Value);
            }
            
            // Default fallback
            return 1; 
        }
    }
}