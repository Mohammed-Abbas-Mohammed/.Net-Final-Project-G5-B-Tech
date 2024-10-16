using ApplicationB.Contracts_B.General;
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
    public class LanguageRepository :ILanguageRepository
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
    }
}
