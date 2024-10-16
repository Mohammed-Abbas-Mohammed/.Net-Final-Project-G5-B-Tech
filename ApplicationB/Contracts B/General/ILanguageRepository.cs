using ModelsB.Localization_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Contracts_B.General
{
    public interface ILanguageRepository
    {
        Task<LanguageB> GetByCodeAsync(string code);
        Task<List<LanguageB>> GetAllAsync();
    }
}
