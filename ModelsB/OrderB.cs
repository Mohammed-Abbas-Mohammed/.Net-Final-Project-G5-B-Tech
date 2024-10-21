using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsB.Product;

namespace ModelsB
{
    public class OrderB
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "money")]
        public decimal TotalAmount { get; set; }
        public Status CurrentStatus { get; set; } = Status.NoAction;
        [Column(TypeName = "money")]
        public decimal DiscountAmount { get; set; } 
        public int? DiscountId { get; set; }
        public string? CouponCode { get; set; } 
        /*[MaxLength(100)]
         public string ShippingAddress { get; set; }  at shipping model
         public string PaymentMethod { get; set; }
         public string ShippingMethod { get; set; }
         public DateTime? ShippedDate { get; set; }*/
        public int CustomerMAFPId { get; set; }  
        public CustomerB Customer { get; set; }
        public ICollection<ProductB> Products { get; set; }  // Relationship with OrderItems
        [ForeignKey("SellerMAFP")]
        public int SellerMAFPId { get; set; }
        public SellerB Seller { get; set; }

        public int? PaymentId { get; set; } 
        [ForeignKey("PaymentMAFP")]
        public PaymentB Payment { get; set; } 

        public enum Status
        {
            NoAction,
            InCart,
            Pending,
            Shipped,
            Delivered,
            Cancelled
        }
    }

    
}
