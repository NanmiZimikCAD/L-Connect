using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using L_Connect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using L_Connect.Models.ViewModels.Client;
using System.Linq;
using System.Threading.Tasks;

// ClientController handles client-specific features like viewing shipments and managing their profile

namespace L_Connect.Controllers
{
    public class ClientController : Controller
    {
        private readonly IShipmentService _shipmentService;

        public ClientController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        public async Task<IActionResult> Dashboard()
        {
            // Check if the user is authenticated and has the NameIdentifier claim
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                // Optionally, log the issue or redirect to login
                return RedirectToAction("Login", "Account");
            }

            int clientId = int.Parse(userIdClaim.Value);

            var clientShipments = await _shipmentService.GetShipmentsByClientIdAsync(clientId);
            var viewModel = new ClientDashboardViewModel
            {
                ActiveShipments = clientShipments.Where(s => s.CurrentStatus != "Delivered").ToList(),
                CompletedShipments = clientShipments.Where(s => s.CurrentStatus == "Delivered").ToList(),
                PendingShipments = clientShipments.Where(s => s.CurrentStatus == "Processing").ToList()
            };

            return View(viewModel);
        }

    }
}