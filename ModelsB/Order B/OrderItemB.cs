using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsB.Order_B
{
    public class OrderItemB
    {
        public int Id { get; set; }

        [ForeignKey("OrderB")]
        public int OrderId { get; set; }
        public OrderB Order { get; set; }

        [ForeignKey("ProductB")]
        public int ProductId { get; set; }
        public ProductB Product { get; set; }

        public int Quantity { get; set; }
       
    }
}
