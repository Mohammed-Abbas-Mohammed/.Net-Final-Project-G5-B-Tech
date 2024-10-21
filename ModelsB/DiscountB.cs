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
    public class DiscountB
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Name { get; set; } // Name of the discount (e.g., "Summer Sale", "Black Friday")
        [MaxLength(150)]
        public string DiscountCode { get; set; } // Optional: a code customers must use to get the discount
        public decimal? DiscountPercentage { get; set; } // Percentage off (e.g., 10%)
        [Column(TypeName = "money")]
        public decimal? DiscountAmount { get; set; } // Fixed amount off (e.g., $10 off)
        public bool IsActive { get; set; } // Whether the discount is currently active
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ProductB> ApplicableProducts { get; set; } // Products that the discount applies to
    }
}
