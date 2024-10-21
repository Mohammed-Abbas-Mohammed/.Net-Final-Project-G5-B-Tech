using ApplicationB.Contracts_B;
using DbContextB;
using Microsoft.EntityFrameworkCore;
using ModelsB.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureB.General
{
    public class GenericRepositoryB<T> : IGenericRepositoryB<T> where T : class
    {
        protected readonly BTechDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepositoryB(BTechDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IQueryable<T>> GetAllAsync() => await Task.FromResult(_dbSet.Select(p => p));

        public virtual async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

