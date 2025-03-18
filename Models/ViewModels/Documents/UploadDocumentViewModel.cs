using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using L_Connect.Models.Domain;

namespace L_Connect.Models.ViewModels.Documents
{
    public class UploadDocumentViewModel
    {
        public int ShipmentID { get; set; }
        
        [Required(ErrorMessage = "Please select a document type")]
        [Display(Name = "Document Type")]
        public int DocumentTypeID { get; set; }
        
        [Required(ErrorMessage = "Please select a file to upload")]
        [Display(Name = "File")]
        public IFormFile File { get; set; }
        
        public IEnumerable<DocumentType> AvailableDocumentTypes { get; set; }
    }
}