using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//User model class that maps to the "USERS" database table, storing essential user information like email, password hash, name, contact details, and their assigned role - it's the core entity for managing user accounts in the L-Connect system.

namespace L_Connect.Models.Domain
{
    [Table("USERS")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Required]
        [Column("full_name")]
        public string FullName { get; set; }

        [Column("contact_number")]
        public string ContactNumber { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Navigation property
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }
    }
}