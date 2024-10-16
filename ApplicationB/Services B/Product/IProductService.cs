using DTOsB.Product;
using DTOsB.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.Product
{
    public interface IProductService
    {
        Task<ResultView<ProductDto>> CreateProductAsync(ProductDto productDto);
        Task<ResultView<ProductDto>> UpdateProductAsync(ProductDto productDto);
        Task<ResultView<ProductDto>> DeleteProductAsync(int id);
        Task<ResultView<ProductDto>> GetProductByIdAsync(int id);
        Task<IQueryable<ProductDto>> GetAllProductsAsync();
        Task<ResultView<IQueryable<ProductDto>>> SearchProductsByNameAsync(string name);
        public Task<EntityPaginatedB<ProductDto>> GetAllPaginatedAsync(int pageNumber, int Count);
    }
}
