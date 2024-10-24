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
        [MaxLength(50)]
        public string CategoryName { get; set; }
        [MaxLength(150)]
        public string? Description { get; set; }
        public int? LanguageId { get; set; }
        public bool IsMainCategory { get; set; }

    }
}
