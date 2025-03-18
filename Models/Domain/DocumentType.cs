using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.Domain
{
    public class DocumentType
    {
        public int DocumentTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public string AllowedExtensions { get; set; } // Comma-separated list of allowed extensions
        
        // Navigation property
        public ICollection<Document> Documents { get; set; }
    }
}