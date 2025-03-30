using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace L_Connect.Models.Domain
{
    [Table("PRICING")]
    public class Pricing
    {
        [Key]
        [Column("price_id")]
        public int PriceId { get; set; }

        [Required]
        [Column("origin_location")]
        [StringLength(100)]
        public string OriginLocation { get; set; }

        [Required]
        [Column("destination_location")]
        [StringLength(100)]
        public string DestinationLocation { get; set; }

        [Required]
        [Column("transportation_method")]
        [StringLength(50)]
        public string TransportationMethod { get; set; }

        [Required]
        [Column("base_rate", TypeName = "decimal(10, 2)")]
        public decimal BaseRate { get; set; }

        [Required]
        [Column("weight_rate", TypeName = "decimal(10, 2)")]
        public decimal WeightRate { get; set; }

        [Required]
        [Column("bulk_threshold")]
        public int BulkThreshold { get; set; }

        [Required]
        [Column("bulk_discount_rate", TypeName = "decimal(5, 2)")]
        public decimal BulkDiscountRate { get; set; }

        [Required]
        [Column("custom_service_charge", TypeName = "decimal(10, 2)")]
        public decimal CustomServiceCharge { get; set; }

        [Required]
        [Column("insurance_charge", TypeName = "decimal(10, 2)")]
        public decimal InsuranceCharge { get; set; }

        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("last_updated_by")]
        public int? LastUpdatedBy { get; set; }

        [Column("last_updated_at")]
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        [ForeignKey("LastUpdatedBy")]
        public virtual User UpdatedByUser { get; set; }
    }
}