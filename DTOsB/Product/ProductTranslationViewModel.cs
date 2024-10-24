namespace DTOsB.Product
{
    public class ProductTranslationViewModel
    {
        public int LanguageId { get; set; } // e.g., "en", "ar"
        public string Name { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
    }
}
