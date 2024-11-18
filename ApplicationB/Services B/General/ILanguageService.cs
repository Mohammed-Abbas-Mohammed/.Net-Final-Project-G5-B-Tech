﻿using DTOsB.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.General
{
    public interface ILanguageService
    {
        int GetCurrentLanguageCode();
        void SetCurrentLanguageCode(int languageId);
        void SetCurrentLanguageCode(string languageCode);

        Task<ResultView<LanguageDto>> CreateProductAsync(LanguageDto langDto);
        Task<ResultView<LanguageDto>> UpdateProductAsync(LanguageDto langDto);
        Task<ResultView<LanguageDto>> DeletelangAsync(int id);
        Task<string> SetLanguageFromBrowserAsync(string acceptLanguageHeader);
        Task<int> SetUserSelectedLanguageAsync(int languageId);
        Task<List<LanguageDto>> GetAllLanguagesAsync();

        Task<ResultView<LanguageDto>> GetLangByIdAsync(int id);
    }
}
