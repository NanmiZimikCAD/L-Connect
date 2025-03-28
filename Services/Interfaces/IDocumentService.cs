using L_Connect.Models.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace L_Connect.Services.Interfaces
{
    public interface IDocumentService
    {
        Task<IEnumerable<DocumentType>> GetDocumentTypesAsync();
        Task<int> UploadDocumentAsync(IFormFile file, int shipmentId, int documentTypeId, int uploadedBy);
        Task<Document> GetDocumentAsync(int documentId);
        Task<IEnumerable<Document>> GetDocumentsByShipmentAsync(int shipmentId);
        Task<(byte[] FileContents, string ContentType, string FileName)> DownloadDocumentAsync(int documentId);
        Task<bool> DeleteDocumentAsync(int documentId, int userId);
        Task<bool> UserCanAccessDocument(int documentId, int userId);
    }
}