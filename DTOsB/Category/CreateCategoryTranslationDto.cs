using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Category
{
    public class CreateCategoryTranslationDto
    {
        [MaxLength(100)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
