using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models.ViewModels.Documents
{
    public class DocumentViewModel
    {
        public int DocumentID { get; set; }
        public string FileName { get; set; }
        public string DocumentTypeName { get; set; }
        public string ContentType { get; set; }
        public int FileSize { get; set; }
        public string FileSizeFormatted { get; set; }
        public string UploadedByName { get; set; }
        public DateTime UploadedDate { get; set; }
    }
}