using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsB.Product;

namespace ModelsB
{
    public class ProductOrderB
    {
        public int Id { get; set; }
        [ForeignKey("ProductMAFP")]
        public int ProductMAFPId { get; set; } 
        public ProductB Product { get; set; }
        [ForeignKey("OrderMAFP")]
        public int OrderMAFPID { get; set; }  
        public OrderB Order { get; set; }
        public int Quantity { get; set; }
    }
}
