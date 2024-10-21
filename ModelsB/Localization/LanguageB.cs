using ModelsB.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsB.Localization
{
    public class LanguageB
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<ProductTranslationB> ProductTranslations { get; set; }
        public ICollection<ProductSpecificationTranslationB> productSpecificationTranslations { get; set; }
    }
}
