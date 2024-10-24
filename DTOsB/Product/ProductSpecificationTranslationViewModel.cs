namespace DTOsB.Product
{
    public class ProductSpecificationTranslationViewModel
    {
        public int LanguageId { get; set; }
        public string TranslatedKey { get; set; } // Specification key (e.g., "Color", "Weight")
        public string TranslatedValue { get; set; } // Specification value (e.g., "Red", "1.5 kg")
    }
}
