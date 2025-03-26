using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using L_Connect.Models;
using L_Connect.Models.ViewModels.Documents;
using L_Connect.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace L_Connect.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IShipmentService _shipmentService;
        
        public DocumentController(IDocumentService documentService, IShipmentService shipmentService)
        {
            _documentService = documentService;
            _shipmentService = shipmentService;
        }
        
        // GET: Document/Index/{shipmentId}
        // For admins to view documents for a shipment
        // GET: Document/Index/{shipmentId}
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Index(int shipmentId)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(shipmentId);
            if (shipment == null)
                return NotFound();
                
            var documents = await _documentService.GetDocumentsByShipmentAsync(shipmentId);
            
            var viewModel = new DocumentListViewModel
            {
                ShipmentID = shipmentId,
                ShipmentTrackingNumber = shipment.TrackingNumber,
                Documents = documents.Select(d => new DocumentViewModel
                {
                    DocumentID = d.DocumentID,
                    FileName = d.OriginalFileName,
                    // Use null conditional operators to handle possible null values
                    DocumentTypeName = d.DocumentType?.TypeName ?? "Unknown Type",
                    ContentType = d.ContentType ?? "application/octet-stream",
                    FileSize = d.FileSize,
                    FileSizeFormatted = Utils.FormatFileSize(d.FileSize),
                    UploadedByName = d.UploadedByUser?.FullName ?? "Unknown User",
                    UploadedDate = d.UploadedDate
                })
            };
            
            return View(viewModel);
        }
        
        // GET: Document/ClientView/{shipmentId}
        // For clients to view documents for their shipment
        // [Authorize(Roles = "CLIENT")]
        // public async Task<IActionResult> ClientView(int shipmentId)
        // {
        //     // Get current user ID
        //     int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
        //     // Get shipment and verify ownership
        //     var shipment = await _shipmentService.GetShipmentByIdAsync(shipmentId);
        //     if (shipment == null || shipment.ClientId != userId)
        //         return Forbid();
                
        //     var documents = await _documentService.GetDocumentsByShipmentAsync(shipmentId);
            
        //     var viewModel = new DocumentListViewModel
        //     {
        //         ShipmentID = shipmentId,
        //         ShipmentTrackingNumber = shipment.TrackingNumber,
        //         Documents = documents.Select(d => new DocumentViewModel
        //         {
        //             DocumentID = d.DocumentID,
        //             FileName = d.OriginalFileName,
        //             DocumentTypeName = d.DocumentType.TypeName,
        //             ContentType = d.ContentType,
        //             FileSize = d.FileSize,
        //             FileSizeFormatted = Utils.FormatFileSize(d.FileSize),
        //             UploadedByName = d.UploadedByUser.FullName,
        //             UploadedDate = d.UploadedDate
        //         })
        //     };
            
        //     return View(viewModel);
        // }
        
        // GET: Document/Upload/{shipmentId}
        // Form for uploading a document (admin only)
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Upload(int shipmentId)
        {
            var shipment = await _shipmentService.GetShipmentByIdAsync(shipmentId);
            if (shipment == null)
                return NotFound();
                
            var documentTypes = await _documentService.GetDocumentTypesAsync();
            
            var viewModel = new UploadDocumentViewModel
            {
                ShipmentID = shipmentId,
                AvailableDocumentTypes = documentTypes
            };
            
            return View(viewModel);
        }
        
        // POST: Document/Upload
        // Process document upload (admin only)
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadDocumentViewModel model)
        {
            Console.WriteLine("DocumentController.Upload POST called");

            // We should populate AvailableDocumentTypes regardless of validation state
            model.AvailableDocumentTypes = await _documentService.GetDocumentTypesAsync();
            // Check if the file exists
            if (model.File == null)
            {
                Console.WriteLine("ERROR: No file attached to upload");
                ModelState.AddModelError("File", "Please select a file to upload");
                //model.AvailableDocumentTypes = await _documentService.GetDocumentTypesAsync();
                return View(model);
            }
            
            Console.WriteLine($"File details: Name={model.File.FileName}, Size={model.File.Length}, Type={model.File.ContentType}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("Model state is invalid");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
                //model.AvailableDocumentTypes = await _documentService.GetDocumentTypesAsync();
                return View(model);
                // model.AvailableDocumentTypes = await _documentService.GetDocumentTypesAsync();
                // return View(model);
            }

            // Print all available claims to find which one contains the user ID
            Console.WriteLine("Available claims:");
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim type: {claim.Type}, Value: {claim.Value}");
            }
            
            // Get current user ID
            //int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Console.WriteLine($"User ID Claim: {userIdClaim}");
            
            // if (string.IsNullOrEmpty(userIdClaim))
            // {
            //     Console.WriteLine("ERROR: User ID claim is missing");
            //     ModelState.AddModelError("", "User ID not found");
            //     model.AvailableDocumentTypes = await _documentService.GetDocumentTypesAsync();
            //     return View(model);
            // }
            
            // int userId = int.Parse(userIdClaim);
            // Console.WriteLine($"User ID: {userId}");

            // Try alternative ways to get the user ID
            int userId;
            var nameIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var sidClaim = User.FindFirst(ClaimTypes.Sid);
            var nameValue = User.Identity?.Name;
            
            if (nameIdClaim != null)
            {
                userId = int.Parse(nameIdClaim.Value);
                Console.WriteLine($"Using NameIdentifier claim: {userId}");
            }
            else if (sidClaim != null)
            {
                userId = int.Parse(sidClaim.Value);
                Console.WriteLine($"Using Sid claim: {userId}");
            }
            else if (!string.IsNullOrEmpty(nameValue) && int.TryParse(nameValue, out userId))
            {
                Console.WriteLine($"Using Identity.Name: {userId}");
            }
            else
            {
                // Hardcode admin user ID temporarily for testing
                userId = 1; // Assuming ID 1 is your admin user
                Console.WriteLine($"WARNING: Using hardcoded admin ID: {userId}");
            }
            
            try
            {
                Console.WriteLine($"About to call _documentService.UploadDocumentAsync with ShipmentID={model.ShipmentID}, DocumentTypeID={model.DocumentTypeID}");

                // Debug check - make sure service is not null
                if (_documentService == null)
                {
                    Console.WriteLine("ERROR: _documentService is null!");
                    throw new InvalidOperationException("Document service is not available");
                }

                await _documentService.UploadDocumentAsync(model.File, model.ShipmentID, model.DocumentTypeID, userId);
                Console.WriteLine("File upload completed successfully");
        
                TempData["SuccessMessage"] = "Document uploaded successfully";
                return RedirectToAction(nameof(Index), new { shipmentId = model.ShipmentID });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXCEPTION in upload: {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                // Just add the exception message to ModelState
                ModelState.AddModelError("", ex.Message);
                model.AvailableDocumentTypes = await _documentService.GetDocumentTypesAsync();
                return View(model);
            }
        }
        
        // Temporary debug code in DocumentController
        public IActionResult TestRole()
        {
            var isAuthenticated = User.Identity.IsAuthenticated;
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();
            
            return Content($"Authenticated: {isAuthenticated}, Roles: {string.Join(", ", roles)}");
        }
        
        // GET: Document/Download/{id}
        // Download a document
        [Authorize]
        public async Task<IActionResult> Download(int id)
        {
            // Get current user ID
            // int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            // Check access permission
            // bool canAccess = await _documentService.UserCanAccessDocument(id, userId);
            // if (!canAccess)
            //     return Forbid();
                
            try
            {
                var (fileContents, contentType, fileName) = await _documentService.DownloadDocumentAsync(id);
                return File(fileContents, contentType, fileName);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        
        // POST: Document/Delete/{id}
        // Delete a document (admin only)
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int shipmentId)
        {
            // Get current user ID
            //int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            // Get current user ID with better error handling
            string userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId;
            
            if (string.IsNullOrEmpty(userIdClaim))
            {
                // Handle missing ID claim
                Console.WriteLine("User ID claim is missing. Using admin fallback ID.");
                
                // For testing, you can use a fallback admin ID
                userId = 1; // Use your admin user ID
            }
            else
            {
                userId = int.Parse(userIdClaim);
            }
            
            bool result = await _documentService.DeleteDocumentAsync(id, userId);
            
            if (result)
                TempData["SuccessMessage"] = "Document deleted successfully";
            else
                TempData["ErrorMessage"] = "Failed to delete document";
                
            return RedirectToAction(nameof(Index), new { shipmentId });
        }
    }
}
