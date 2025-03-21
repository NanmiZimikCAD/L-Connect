using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.Domain
{
    public class Document
    {
        public int DocumentID { get; set; }
        public int ShipmentID { get; set; }
        public int DocumentTypeID { get; set; }
        public string FileName { get; set; } // Name in storage system
        public string OriginalFileName { get; set; } // Original file name
        public string ContentType { get; set; } // MIME type
        public int FileSize { get; set; } // Size in bytes
        public string FilePath { get; set; } // Path to file in storage
        public int UploadedBy { get; set; }
        public DateTime UploadedDate { get; set; }
        
        // Navigation properties
        public Shipment Shipment { get; set; }
        public DocumentType DocumentType { get; set; }
        public User UploadedByUser { get; set; }
    }
}