using ApplicationB.Contracts_B;
using DbContextB;
using DTOsB.Shared;
using InfrastructureB.General;
using Microsoft.EntityFrameworkCore;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureB.Product
{
    public class ProductRepository : GenericRepositoryWithLogging<ProductB>, IProductRepository
    {


        public ProductRepository(BTechDbContext context) : base(context)
        {
            
        }


        public async Task<IQueryable<ProductB>> SearchByNameAsync(string name)
        {
            var products = await GetAllAsync();  
            return products.Where(p => p.Translations.Any(t => t.Name.ToLower().Contains(name.ToLower()) ||
                 p.Translations.Any(t => t.BrandName.ToLower().Contains(name.ToLower()))));
        }

        public override async Task<ProductB> GetByIdAsync(int id)
        {
            return await _context.Products.Where(p => !p.IsDeleted).Include(p => p.Translations).Include(p => p.Images)
                .Include(p => p.Specifications).ThenInclude(s => s.Translations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task<IQueryable<ProductB>> GetAllAsync()
        {
            
            return await Task.FromResult(_context.Products.Where(p => !p.IsDeleted).Include(p => p.Translations)
                .Include(p => p.Images));

        }

        public async Task<IQueryable<ProductB>> GetFilteredProductsAsync(int languageId)
        {
            var products = _context.Products.Include(p => p.Translations);
            return products.Where(p => p.Translations.Any(t => t.Language.Id == languageId));
        }
        public async Task<EntityPaginatedB<ProductB>> GetAllPaginatedAsync(int pageNumber, int count)
        {
           
            if (pageNumber < 1) pageNumber = 1;
            if (count < 1) count = 10; 

         
            var skip = (pageNumber - 1) * count;

            var query = _context.Products 
                                .Include(p => p.Images)       
                                .Include(p => p.Translations) 
                                .Include(p => p.Specifications)
                                .ThenInclude(s => s.Translations)
                                .OrderBy(p => p.Id)   
                                .Skip(skip)                 
                                .Take(count);               

          
            var totalCount = await _context.Products.CountAsync();

           
            var products = await query.ToListAsync();

            
            var result = new EntityPaginatedB<ProductB>
            {
                Data = products,        // Paginated items
                PageNumber = pageNumber,  // Current page number
                Count = count,            // Items per page
                CountAllItems = totalCount   // Total items in the database
            };

            return result;
        }

        //public async Task<IEnumerable<ProductB>> GetProductsByCategoryIdsAsync(IEnumerable<int> categoryIds)
        //{
        //    return await _context.ProductCategories
        //        .Where(pc => categoryIds.Contains(pc.CategoryId))
        //        .Select(pc => pc.Product)
        //        .ToListAsync();
        //}

    }
}