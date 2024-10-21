using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Product
{
    public class CreateOrUpdateProductDTO
    {
        public int ProductMAFPId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public List<string> ImageUrl { get; set; }
        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }
        public int CategoryMAFPId { get; set; }
        public int SellerMAFPId { get; set; }
    }
}
