using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationB.Contracts_B;
using DbContextB;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureB
{
    public class GenericRepositoryB<T> : IGenericRepositoryB<T> where T : class
    {
        private readonly BTechDbContext _dbcontext;
        private readonly DbSet<T> dbset;

        public GenericRepositoryB(BTechDbContext dbContext) {
            _dbcontext=dbContext;
            dbset=_dbcontext.Set<T>();

        }
        public async Task AddAsync(T entity)
        {
           await dbset.AddAsync(entity);      
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                dbset.Remove(entity);
            }

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            dbset.Update(entity);
        }
    }
}
