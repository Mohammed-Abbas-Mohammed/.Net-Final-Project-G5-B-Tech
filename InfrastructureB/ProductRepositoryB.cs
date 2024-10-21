
using DbContextB;
using ModelsB.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureB
{
    public class ProductRepositoryB: GenericRepositoryB<ProductB>
    {
        public ProductRepositoryB(BTechDbContextB dbContext): base(dbContext) 
        {
            
        }
    }
}
