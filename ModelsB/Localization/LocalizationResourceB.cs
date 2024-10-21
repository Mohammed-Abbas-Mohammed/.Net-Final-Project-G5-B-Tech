using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsB.Localization
{
    public class LocalizationResourceB
    {
        [Key]
        public string Key { get; set; }
        public string ArabicValue { get; set; }
        public string EnglishValue { get; set; }
    }
}
