using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Services
{
    public class DocumentStorageOptions
    {
        public string LocalPath { get; set; }
        public long MaxFileSize { get; set; } = 10 * 1024 * 1024; // Default 10MB
    }
}