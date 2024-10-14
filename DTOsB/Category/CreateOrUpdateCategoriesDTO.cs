using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Category
{
    public class CreateOrUpdateCategoriesDTO
    {
        
        public string ImageUrl { get; set; }
        public List<CreateCategoryTranslationDto> Translations { get; set; }
    }
}
