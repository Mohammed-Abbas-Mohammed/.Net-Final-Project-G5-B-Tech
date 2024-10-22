﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationB.Contracts_B;
using DbContextB;
using Microsoft.EntityFrameworkCore;
using ModelsB.Shared;

namespace InfrastructureB.General
{
    public class GenericRepositoryWithLogging<T>: IGenericRepositoryB<T> where T : BaseEntityB
    {
        protected readonly BTechDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepositoryWithLogging(BTechDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual Task<IQueryable<T>> GetAllAsync() => Task.FromResult(_dbSet.Select(p => p).Where(p => !p.IsDeleted));

        public virtual async Task<T> GetByIdAsync(int id) => await _dbSet.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

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
                entity.IsDeleted = true; ;
                await UpdateAsync(entity);
            }
        }
    }
}
