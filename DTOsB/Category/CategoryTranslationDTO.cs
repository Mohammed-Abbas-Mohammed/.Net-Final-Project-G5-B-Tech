using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOsB.Category
{
    public record CategoryTranslationDTO
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string CategoryName { get; set; }

        [MaxLength(150)]
        public string? Description { get; set; }
    }
}
