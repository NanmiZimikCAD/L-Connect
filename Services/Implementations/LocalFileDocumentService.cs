using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Data;
using L_Connect.Models.Domain;
using L_Connect.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace L_Connect.Services.Implementations
{
    public class LocalFileDocumentService : IDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _basePath;
        private readonly long _maxFileSize;

        public LocalFileDocumentService(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            // _basePath = options.Value.LocalPath;
            // _maxFileSize = options.Value.MaxFileSize;
            _basePath = configuration["DocumentStorage:LocalPath"] ?? "C:\\xampp\\document_storage";
            
            // Ensure base storage directory exists
            if (!Directory.Exists(_basePath))
                Directory.CreateDirectory(_basePath);
        }

        public async Task<IEnumerable<DocumentType>> GetDocumentTypesAsync()
        {
            return await _context.DocumentTypes.ToListAsync();
        }

        public async Task<int> UploadDocumentAsync(IFormFile file, int shipmentId, int documentTypeId, int uploadedBy)
        {
            Console.WriteLine("LocalFileDocumentService.UploadDocumentAsync called");
            Console.WriteLine($"File info: Name={file.FileName}, Size={file.Length}");
            Console.WriteLine($"Settings: BasePath={_basePath}");
            
            // Create a simple directory structure
            string documentsFolder = Path.Combine(_basePath, "documents");
            Console.WriteLine($"Documents folder: {documentsFolder}");
            
            try {
                Directory.CreateDirectory(documentsFolder);
                Console.WriteLine("Documents directory created/verified");
                
                // Create a unique filename
                string uniqueFileName = $"{DateTime.Now.Ticks}_{file.FileName}";
                string filePath = Path.Combine(documentsFolder, uniqueFileName);
                Console.WriteLine($"Target file path: {filePath}");
                
                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    Console.WriteLine("Copying file...");
                    await file.CopyToAsync(stream);
                    Console.WriteLine("File copied successfully");
                }
                
                // Save metadata to database
                Console.WriteLine("Creating document record in database");
                var document = new Document
                {
                    ShipmentID = shipmentId,
                    DocumentTypeID = documentTypeId,
                    FileName = uniqueFileName,
                    OriginalFileName = file.FileName,
                    ContentType = file.ContentType,
                    FileSize = (int)file.Length,
                    FilePath = filePath,
                    UploadedBy = uploadedBy,
                    UploadedDate = DateTime.UtcNow
                };
                
                _context.Documents.Add(document);
                await _context.SaveChangesAsync();
                
                Console.WriteLine($"Document saved with ID: {document.DocumentID}");
                return document.DocumentID;
            }
            catch (Exception ex) {
                Console.WriteLine($"ERROR in document service: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Rethrow to let the controller handle it
            }
        }

        public async Task<Document> GetDocumentAsync(int documentId)
        {
            // return await _context.Documents
            //     .Include(d => d.DocumentType)
            //     .Include(d => d.UploadedByUser)
            //     .FirstOrDefaultAsync(d => d.DocumentID == documentId);
            return await _context.Documents.FindAsync(documentId);
        }

        public async Task<IEnumerable<Document>> GetDocumentsByShipmentAsync(int shipmentId)
        {
            // return await _context.Documents
            //     .Where(d => d.ShipmentID == shipmentId)
            //     .Include(d => d.DocumentType)
            //     .Include(d => d.UploadedByUser)
            //     .OrderByDescending(d => d.UploadedDate)
            //     .ToListAsync();
            //return await _context.Documents.Where(d => d.ShipmentID == shipmentId).ToListAsync();
            return await _context.Documents
            .Where(d => d.ShipmentID == shipmentId)
            .Include(d => d.DocumentType)  // Include related DocumentType
            .Include(d => d.UploadedByUser)  // Include related User
            .OrderByDescending(d => d.UploadedDate)
            .ToListAsync();
        }

        public async Task<(byte[] FileContents, string ContentType, string FileName)> DownloadDocumentAsync(int documentId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
                throw new FileNotFoundException("Document not found");
                
            // if (!File.Exists(document.FilePath))
            //     throw new FileNotFoundException("Document file not found");
                
            byte[] fileBytes = await File.ReadAllBytesAsync(document.FilePath);
            
            return (fileBytes, document.ContentType, document.OriginalFileName);
        }

        public async Task<bool> DeleteDocumentAsync(int documentId, int userId)
        {
            var document = await _context.Documents.FindAsync(documentId);
            if (document == null)
                return false;
                
            // // Only allow deletion by admin or the user who uploaded it
            // var user = await _context.Users.FindAsync(userId);
            // if (user == null)
            //     return false;

            // // Get admin role ID
            // int adminRoleId = await _context.Roles
            //     .Where(r => r.RoleName == "Admin")
            //     .Select(r => r.RoleId)
            //     .FirstOrDefaultAsync();
                
            // if (user.RoleId != adminRoleId && document.UploadedBy != userId)
            //     return false;
                
            // Delete the physical file
            if (File.Exists(document.FilePath))
            {
                File.Delete(document.FilePath);
            }
            
            // Remove database record
            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> UserCanAccessDocument(int documentId, int userId)
        {
            // var document = await _context.Documents
            //     .Include(d => d.Shipment)
            //     .FirstOrDefaultAsync(d => d.DocumentID == documentId);
                
            // if (document == null)
            //     return false;
                
            // var user = await _context.Users.FindAsync(userId);
            // if (user == null)
            //     return false;
                
            // // Get role IDs
            // int adminRoleId = await _context.Roles
            //     .Where(r => r.RoleName == "Admin")
            //     .Select(r => r.RoleId)
            //     .FirstOrDefaultAsync();
                
            // int clientRoleId = await _context.Roles
            //     .Where(r => r.RoleName == "Client")
            //     .Select(r => r.RoleId)
            //     .FirstOrDefaultAsync();
                
            // // Admins can access all documents
            // if (user.RoleId == adminRoleId)
            //     return true;
                
            // // Clients can only access documents for their shipments
            // if (user.RoleId == clientRoleId)
            // {
            //     return document.Shipment.ClientId == userId;
            // }
            
            //return false;
            return true;
        }
    }
}
