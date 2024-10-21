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
    public class ReviewB
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int? Rating { get; set; }  // 1-5 stars
        [MaxLength(200)]
        public string? Comment { get; set; }
        public DateTime DatePosted { get; set; }
        [ForeignKey("ProductMAFP")]
        public int ProductMAFPId { get; set; }  // Foreign key for Product
        public ProductB Product { get; set; }
        [ForeignKey("CustomerMAFP")]
        public int CustomerMAFPId { get; set; }  // Foreign key for User
        public CustomerB Customer { get; set; }
      
        
    }
}
