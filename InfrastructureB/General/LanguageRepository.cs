﻿using ApplicationB.Contracts_B.General;
using ApplicationB.Services_B.General;
using DbContextB;
using Microsoft.EntityFrameworkCore;
using ModelsB.Localization_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureB.General
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly BTechDbContext _context;

        public LanguageRepository(BTechDbContext context)
        {
            _context = context;
        }

        public async Task<LanguageB> GetByCodeAsync(string code)
        {
            return await _context.Languages.FirstOrDefaultAsync(l => l.Code == code);
        }

        public async Task<List<LanguageB>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<LanguageB> GetByIdAsync(int Id)
        {
            return await _context.Languages.FirstOrDefaultAsync(l => l.Id == Id);
        }

        public async Task<bool> AnyAsync(Func<LanguageB, bool> predicate)
        {
            return await Task.FromResult(_context.Languages.Any(predicate));
        }

        public async Task<LanguageB> AddAsync(LanguageB entity)
        {
            var recieved = (await _context.Languages.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return recieved;
        }

        public async Task UpdateAsync(LanguageB entity)
        {
            _context.Languages.Update(entity);
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
