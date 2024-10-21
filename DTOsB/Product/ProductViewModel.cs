namespace DTOsB.Product
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
       
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public string CreatedBy { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string UpdatedBy { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }

        // Images
        public List<ProductImageViewModel> Images { get; set; }

        // Translations (for languages)
        public List<ProductTranslationViewModel> Translations { get; set; }

        // Specifications
        public List<ProductSpecificationViewModel> Specifications { get; set; }
    }
}

