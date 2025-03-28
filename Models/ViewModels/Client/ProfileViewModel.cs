using System.ComponentModel.DataAnnotations;

namespace L_Connect.Models.ViewModels.Client
{
    public class ProfileViewModel
    {
        [Required]
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public string ContactNumber { get; set; }
    }
}
