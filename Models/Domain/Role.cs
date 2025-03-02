using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Role model class that maps to the "ROLES" database table, storing user role information like Admin or Client, with properties for role ID, name, description, creation date, and a relationship to User entities - it's essentially the blueprint for managing different user roles in the L-Connect system.

namespace L_Connect.Models.Domain
{
    [Table("ROLES")]
    public class Role
    {
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required]
        [Column("role_name")]
        public string RoleName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Navigation property
        public virtual ICollection<User> Users { get; set; }
    }
}