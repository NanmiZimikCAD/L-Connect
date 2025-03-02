using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;// Import base MVC controller functionality
using Microsoft.Extensions.Logging;
//using L_Connect.Models.ViewModels.Tracking; // Import the tracking view models
using L_Connect.Services.Interfaces; // Import the service interfaces
//using System.Threading.Tasks; // Import Task for async operations
using System.Linq; // Import LINQ for working with collections
using System.Collections.Generic;
using L_Connect.Models.ViewModels.Tracking; // Import for List<T>

namespace L_Connect.Controllers
{
    // Controller for handling all tracking-related functionality
    public class TrackingController : Controller
    {
        // Private field to store the shipment service
        private readonly IShipmentService _shipmentService;

        // Constructor that receives the shipment service via dependency injection
        public TrackingController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService; // Store the injected service for later use
        }

        // GET: /Tracking - Displays the tracking form
        [HttpGet] // Specifies this action only responds to HTTP GET requests
        public IActionResult Index()
        {
            // Return the view with a new empty tracking view model
            return View(new TrackingViewModel());
        }

        // POST: /Tracking - Processes the tracking form submission
        [HttpPost] // Specifies this action only responds to HTTP POST requests
        [ValidateAntiForgeryToken] // Prevents Cross-Site Request Forgery attacks
        public async Task<IActionResult> Index(TrackingViewModel model)
        {
            // Check if the model passed validation (based on data annotations)
            if (!ModelState.IsValid)
            {
                return View(model); // If invalid, return to the same view with the model to show errors
            }

            // Validate that the tracking number exists in the database
            bool isValid = await _shipmentService.ValidateTrackingNumberAsync(model.TrackingNumber);
            if (!isValid)
            {
                // Add a custom error message to the ModelState
                ModelState.AddModelError("TrackingNumber", "Invalid tracking number. Please check and try again.");
                return View(model); // Return to the form with the error message
            }

            // Redirect to the Result action, passing the tracking number
            // nameof() ensures refactoring safety if the method name changes
            return RedirectToAction(nameof(Result), new { trackingNumber = model.TrackingNumber });
        }

        // GET: /Tracking/Result/{trackingNumber} - Displays tracking results
        [HttpGet]
        public async Task<IActionResult> Result(string trackingNumber)
        {
            // Check if tracking number was provided
            if (string.IsNullOrEmpty(trackingNumber))
            {
                return RedirectToAction(nameof(Index)); // Redirect to the form if no tracking number
            }

            // Retrieve the shipment from the database using the service
            var shipment = await _shipmentService.GetShipmentByTrackingNumberAsync(trackingNumber);
            if (shipment == null)
            {
                // If no shipment found, show the NotFound view
                return View("NotFound", trackingNumber);
            }

            // Create and populate the view model for the tracking result
            var viewModel = new TrackingResultViewModel
            {
                TrackingNumber = shipment.TrackingNumber, // Set the tracking number
                CurrentStatus = shipment.CurrentStatus, // Set current shipment status
                OriginAddress = shipment.OriginAddress, // Set origin address
                DestinationAddress = shipment.DestinationAddress, // Set destination address
                CurrentLocation = shipment.CurrentLocation, // Set current location
                CreatedAt = shipment.CreatedAt, // Set creation date
                EstimatedDeliveryDate = shipment.EstimatedDeliveryDate, // Set estimated delivery date
                // Check if user is authenticated and has CLIENT role to determine document access
                CanAccessDocuments = User.Identity.IsAuthenticated && User.IsInRole("CLIENT"),
                StatusHistory = new List<TrackingResultViewModel.StatusUpdateInfo>() // Initialize empty list
            };

            // Retrieve the shipment status history from the database
            var statusHistory = await _shipmentService.GetShipmentsByStatusHistoryAsync(shipment.ShipmentId);
            if (statusHistory != null && statusHistory.Count > 0)
            {
                // Map each status update to the view model's StatusUpdateInfo class
                viewModel.StatusHistory = statusHistory.Select(s => new TrackingResultViewModel.StatusUpdateInfo
                {
                    Status = s.Status, // Set status text
                    Location = s.Location, // Set location
                    Notes = s.Notes, // Set notes
                    UpdatedAt = s.UpdatedAt // Set update timestamp
                }).ToList();
            }

            // Return the Result view with the populated view model
            return View(viewModel);
        }

    }
}