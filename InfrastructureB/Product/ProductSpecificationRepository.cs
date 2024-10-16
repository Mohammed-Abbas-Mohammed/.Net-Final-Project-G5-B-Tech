using ApplicationB.Contracts_B.Product;
using DbContextB;
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
    //public class ProductSpecificationRepository : GenericRepositoryWithLogging<ProductSpecificationsB>, IProductSpecificationRepository
    //{
    //    public ProductSpecificationRepository(BTechDbContext context) : base(context) { }

    //    public IQueryable<ProductSpecificationsB> GetSpecificationsByProductId(int productId)
    //    {
    //        return GetAll().Where(spec => spec.ProductId == productId);
    //    }
    
    //}
}
