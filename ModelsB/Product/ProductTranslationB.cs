using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsB.Shared;

namespace ModelsB.Product
{
    public class ProductTranslationB :BaseTranslationB
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [ForeignKey("ProductB")]
        public int ProductId { get; set; }

        public ProductB Product { get; set; }
    }
}
