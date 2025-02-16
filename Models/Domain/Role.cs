using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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