using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using L_Connect.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using L_Connect.Models.Domain;
using L_Connect.Models.ViewModels.Client;
using L_Connect.Models.ViewModels.Documents;
using L_Connect.Models;

namespace L_Connect.Controllers
{
    public class ClientController : Controller
    {
        private readonly IShipmentService _shipmentService;
        private readonly IDocumentService _documentService;
        private readonly IUserService _userService;

        public ClientController(
            IShipmentService shipmentService,
            IDocumentService documentService,
            IUserService userService)
        {
            _shipmentService = shipmentService;
            _documentService = documentService;
            _userService = userService;
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                // Get current client ID
                int clientId = GetCurrentClientId();
                
                // Get all shipments for this client
                var allShipments = await _shipmentService.GetShipmentsByClientIdAsync(clientId);
                
                // Get user information
                User user = null;
                if (User.Identity.IsAuthenticated)
                {
                    user = await _userService.GetUserByEmail(User.Identity.Name);
                }
                
                // Create view model
                var viewModel = new ClientDashboardViewModel
                {
                    ClientName = user?.FullName ?? User.Identity?.Name ?? "Client",
                    TotalShipments = allShipments.Count,
                    DeliveredShipments = allShipments.Count(s => s.CurrentStatus == "Delivered"),
                    InTransitShipments = allShipments.Count(s => s.CurrentStatus == "In Transit"),
                    ProcessingShipments = allShipments.Count(s => 
                        s.CurrentStatus == "Processing" || 
                        s.CurrentStatus == "Pending"),
                    
                    // Get active (non-delivered) shipments
                    ActiveShipments = allShipments
                        .Where(s => s.CurrentStatus != "Delivered" && s.CurrentStatus != "Cancelled")
                        .OrderByDescending(s => s.CreatedAt)
                        .Take(5)
                        .ToList()
                };
                
                // Add recently viewed items (this would normally come from a database)
                // For now, we'll create some mock data based on the most recent shipments
                var recentItems = new List<ClientDashboardViewModel.RecentlyViewedItem>();
                
                foreach (var shipment in allShipments.Take(3))
                {
                    recentItems.Add(new ClientDashboardViewModel.RecentlyViewedItem
                    {
                        ItemType = "Shipment",
                        ItemId = shipment.TrackingNumber,
                        ItemTitle = $"Tracking #{shipment.TrackingNumber}",
                        Status = shipment.CurrentStatus,
                        StatusClass = GetStatusBadgeClass(shipment.CurrentStatus),
                        ViewedDate = DateTime.Now.AddHours(-new Random().Next(1, 72)),
                        AdditionalInfo = $"ETA: {(shipment.EstimatedDeliveryDate?.ToString("MMM dd, yyyy") ?? "Not available")}"
                    });
                }
                
                viewModel.RecentlyViewed = recentItems;
                
                // Add documents if available
                if (_documentService != null)
                {
                    var documents = new List<DocumentViewModel>();
                    
                    // Get documents for each shipment
                    foreach (var shipment in allShipments.Take(5))
                    {
                        try 
                        {
                            var shipmentDocs = await _documentService.GetDocumentsByShipmentAsync(shipment.ShipmentId);
                            
                            documents.AddRange(shipmentDocs.Select(d => new DocumentViewModel
                            {
                                DocumentID = d.DocumentID,
                                FileName = d.OriginalFileName,
                                DocumentTypeName = d.DocumentType?.TypeName ?? "Unknown Type",
                                ContentType = d.ContentType ?? "application/octet-stream",
                                FileSize = d.FileSize,
                                FileSizeFormatted = Utils.FormatFileSize(d.FileSize),
                                UploadedByName = d.UploadedByUser?.FullName ?? "Admin",
                                UploadedDate = d.UploadedDate
                            }));
                        }
                        catch (Exception ex)
                        {
                            // Log error but continue
                            Console.WriteLine($"Error getting documents for shipment {shipment.ShipmentId}: {ex.Message}");
                        }
                    }
                    
                    viewModel.RecentDocuments = documents.OrderByDescending(d => d.UploadedDate).Take(5).ToList();
                }
                
                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error in Dashboard action: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                
                // Return a basic view with minimal data
                return View(new ClientDashboardViewModel
                {
                    ClientName = User.Identity?.Name ?? "Client",
                    ActiveShipments = new List<Shipment>()
                });
            }
        }
        
        // Helper method to get current client ID
        private int GetCurrentClientId()
        {
            // Try to get the user ID from claims
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            
            var nameIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (nameIdClaim != null && int.TryParse(nameIdClaim.Value, out userId))
            {
                return userId;
            }
            
            // Default fallback for development
            return 2; // The client ID from seeded data
        }
        
        // Helper method to get CSS class for status badges
        private string GetStatusBadgeClass(string status)
        {
            return status?.ToLower() switch
            {
                "delivered" => "bg-success",
                "in transit" => "bg-info",
                "processing" => "bg-warning text-dark",
                "pending" => "bg-warning text-dark",
                "cancelled" => "bg-danger",
                _ => "bg-secondary"
            };
        }
    }
}