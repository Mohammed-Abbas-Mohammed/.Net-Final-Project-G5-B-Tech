using DTOsB.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.General
{
    public interface ILanguageService
    {
        string GetCurrentLanguageCode();
        void SetCurrentLanguageCode(string languageCode);
        Task<string> SetLanguageFromBrowserAsync(string acceptLanguageHeader);
        Task<string> SetUserSelectedLanguageAsync(string languageCode); 
        Task<List<LanguageDto>> GetAllLanguagesAsync();
    }
}
