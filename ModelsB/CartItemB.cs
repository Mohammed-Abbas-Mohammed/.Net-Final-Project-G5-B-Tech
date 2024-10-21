using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsB.Product;

namespace ModelsB
{
    //The CartItem model represents an item in the shopping cart.
    public class CartItemB
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        [ForeignKey("ShoppingCartMAFP")]
        public int ShoppingCartId { get; set; } 
        public ShoppingCartB ShoppingCart { get; set; }
        [ForeignKey("ProductMAFP")]
        public int ProductId { get; set; } 
        public ProductB Product { get; set; }
        public int Quantity { get; set; }
    }
}
