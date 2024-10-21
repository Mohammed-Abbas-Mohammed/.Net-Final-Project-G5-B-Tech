using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Product
{
    public class ProductSpecificationTranslationDto
    {
        public int Id { get; set; }
        public string TranslatedKey { get; set; }
        public string TranslatedValue { get; set; }

        public int SpecificationId { get; set; }
    }
}
