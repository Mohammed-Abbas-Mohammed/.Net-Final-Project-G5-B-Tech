using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsB.Category;
using ModelsB.Shared;

namespace ModelsB.Product
{
    
    public class ProductB : BaseEntityB
    {
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        [ForeignKey("CategoryB")]
        public int CategoryId { get; set; }
        public CategoryB Category { get; set; }
        public ICollection<ProductOrderB>? ProductOrders { get; set; }
        public ICollection<ProductImageB> Images { get; set; }
        [ForeignKey("SellerB")]
        public int? SellerId { get; set; }
        public SellerB? Seller { get; set; }
        public ICollection<ProductSpecificationsB> Specifications { get; set; }
        public ICollection<ProductTranslationB> Translations { get; set; }
    }
}
