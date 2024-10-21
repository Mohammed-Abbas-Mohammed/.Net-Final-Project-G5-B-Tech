using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsB
{
    public class CouponB
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Code { get; set; } // Coupon code (e.g., "SAVE10")
        public decimal DiscountAmount { get; set; } // Fixed amount or percentage off
        public DateTime ExpiryDate { get; set; }
        public bool IsRedeemed { get; set; } // Whether the coupon has already been used
        [ForeignKey("CustomerMAFP")]
        public int? CustomerId { get; set; } // Optional: The customer who the coupon belongs to (if it’s user-specific)
    }
}
