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
        [Authorize(Roles = "Admin")]
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
                    DocumentTypeName = d.DocumentType.TypeName,
                    ContentType = d.ContentType,
                    FileSize = d.FileSize,
                    FileSizeFormatted = Utils.FormatFileSize(d.FileSize),
                    UploadedByName = d.UploadedByUser.FullName,
                    UploadedDate = d.UploadedDate
                })
            };
            
            return View(viewModel);
        }
        
        // GET: Document/ClientView/{shipmentId}
        // For clients to view documents for their shipment
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> ClientView(int shipmentId)
        {
            // Get current user ID
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            // Get shipment and verify ownership
            var shipment = await _shipmentService.GetShipmentByIdAsync(shipmentId);
            if (shipment == null || shipment.ClientId != userId)
                return Forbid();
                
            var documents = await _documentService.GetDocumentsByShipmentAsync(shipmentId);
            
            var viewModel = new DocumentListViewModel
            {
                ShipmentID = shipmentId,
                ShipmentTrackingNumber = shipment.TrackingNumber,
                Documents = documents.Select(d => new DocumentViewModel
                {
                    DocumentID = d.DocumentID,
                    FileName = d.OriginalFileName,
                    DocumentTypeName = d.DocumentType.TypeName,
                    ContentType = d.ContentType,
                    FileSize = d.FileSize,
                    FileSizeFormatted = Utils.FormatFileSize(d.FileSize),
                    UploadedByName = d.UploadedByUser.FullName,
                    UploadedDate = d.UploadedDate
                })
            };
            
            return View(viewModel);
        }
        
        // GET: Document/Upload/{shipmentId}
        // Form for uploading a document (admin only)
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(UploadDocumentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableDocumentTypes = await _documentService.GetDocumentTypesAsync();
                return View(model);
            }
            
            // Get current user ID
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            try
            {
                await _documentService.UploadDocumentAsync(model.File, model.ShipmentID, model.DocumentTypeID, userId);
                
                TempData["SuccessMessage"] = "Document uploaded successfully";
                return RedirectToAction(nameof(Index), new { shipmentId = model.ShipmentID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                model.AvailableDocumentTypes = await _documentService.GetDocumentTypesAsync();
                return View(model);
            }
        }
        
        // GET: Document/Download/{id}
        // Download a document
        [Authorize]
        public async Task<IActionResult> Download(int id)
        {
            // Get current user ID
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            // Check access permission
            bool canAccess = await _documentService.UserCanAccessDocument(id, userId);
            if (!canAccess)
                return Forbid();
                
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
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int shipmentId)
        {
            // Get current user ID
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            bool result = await _documentService.DeleteDocumentAsync(id, userId);
            
            if (result)
                TempData["SuccessMessage"] = "Document deleted successfully";
            else
                TempData["ErrorMessage"] = "Failed to delete document";
                
            return RedirectToAction(nameof(Index), new { shipmentId });
        }
    }
}