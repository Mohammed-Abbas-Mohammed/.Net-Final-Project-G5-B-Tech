using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsB.Product;
using ModelsB.Shared;

namespace ModelsB.Category
{
    public class CategoryB : BaseEntityB
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }
        [MaxLength(150)]
        public string? Description { get; set; }
        [MaxLength(250)]
        public string? ImageUrl { get; set; }
        public ICollection<ProductB> Products { get; set; }
    }
}
