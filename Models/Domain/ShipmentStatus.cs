using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L_Connect.Models.Domain
{
    public class ShipmentStatus
    {
        [Key]
        public int StatusId { get; set; }
        
        [Required]
        [ForeignKey("Shipment")]
        public int ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }
        
        [ForeignKey("Admin")]
        public int? UpdatedByAdminId { get; set; }
        public virtual User Admin { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        
        [StringLength(255)]
        public string Location { get; set; }
        
        [StringLength(500)]
        public string Notes { get; set; }
        
        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}