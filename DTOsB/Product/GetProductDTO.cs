using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Product
{
    public record GetProductDTO
    {
        public int ProductMAFPId { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public List<string> ImageUrl { get; init; }
        public int StockQuantity { get; init; }

        public int CategoryMAFPId { get; init; }
        public string CategoryName { get; init; }
    }
}
