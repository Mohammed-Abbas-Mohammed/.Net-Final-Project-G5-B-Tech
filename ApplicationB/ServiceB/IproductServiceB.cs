
using DTOsB.Product;
using ModelsB.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.ServiceB
{
    public interface IproductServiceB: IGenericServiceB<ProductB>
    {
        public Task<List<GetProductDTO>> SearchByNameAsync(string name);

        ////public Task<EntityPaginated<GetBookDTO>> GetAllPaginatedAsync(int pageNumber, int Count);
        ////public Task<CreateOrUpdateBookDTO> GetIdAsync(int id);
    }
}
