using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace L_Connect.Models
{
    public class Utils
    {
        public static string FormatFileSize(int bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB" };
            int counter = 0;
            decimal number = bytes;
            
            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }
            
            return $"{number:n1} {suffixes[counter]}";
        }
    }
}