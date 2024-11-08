using DTOsB.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Product
{
    public class ProductViewModelWithLang
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<LanguageDto> AvailableLanguages { get; set; }
        public int SelectedLanguageId { get; set; }
    }
}
