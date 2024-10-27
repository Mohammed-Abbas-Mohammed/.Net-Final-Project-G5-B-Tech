using DTOsB.Shared;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Contracts_B
{
    public interface IProductRepository: IGenericRepositoryB<ProductB>
    {
        public Task<IQueryable<ProductB>> SearchByNameAsync(string name);


        Task<EntityPaginatedB<ProductB>> GetAllPaginatedAsync(int pageNumber, int count);
        Task<IQueryable<ProductB>> GetFilteredProductsAsync(int languageId);
        //public Task<IEnumerable<ProductB>> GetProductsByCategoryIdsAsync(IEnumerable<int> categoryIds);

    }


    //Task AddImagesAsync(int productId, IEnumerable<ProductImageB> images);
    //Task RemoveImageAsync(int productId, int imageId);
    //Task AddTranslationsAsync(int productId, IEnumerable<ProductTranslationB> translations);
    //Task RemoveTranslationAsync(int productId, int translationId);
    //Task DeleteAsync(int id);

    //Task AddImagesAsync(int productId, IEnumerable<ProductImageB> images);
    //Task RemoveImageAsync(int productId, int imageId);
    //Task AddTranslationsAsync(int productId, IEnumerable<ProductTranslationB> translations);
    //Task RemoveTranslationAsync(int productId, int translationId);

    //Task AddSpecificationsAsync(int productId, IEnumerable<ProductSpecificationsB> specifications);
    //Task RemoveSpecificationAsync(int productId, int specificationId);
    //IQueryable<ProductSpecificationsB> GetSpecificationsByProductId(int productId);
}

