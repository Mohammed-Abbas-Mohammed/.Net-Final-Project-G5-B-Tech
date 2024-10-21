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
    public class PromotionB
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Type { get; set; } // e.g., "BuyOneGetOne", "FreeShipping", "PercentageOff"
        public decimal? DiscountPercentage { get; set; }
        [Column(TypeName = "money")]
        public decimal? DiscountAmount { get; set; }
        public bool AppliesToShipping { get; set; } // Whether the promotion applies to shipping costs
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ICollection<ProductB> ApplicableProducts { get; set; }
    }
}
