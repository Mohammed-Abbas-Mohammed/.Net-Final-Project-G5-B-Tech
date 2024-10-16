using ApplicationB.Contracts_B.General;
using DTOsB.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Services_B.General
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private string _currentLanguageCode;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
            _currentLanguageCode = "en"; // Default to English
        }

        // Get the currently selected language
        public string GetCurrentLanguageCode()
        {
            return _currentLanguageCode;
        }

        // Set language manually (user selection)
        public void SetCurrentLanguageCode(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
            {
                throw new ArgumentException("Invalid language code");
            }

            _currentLanguageCode = languageCode;
        }

        // Handle default language detection from Accept-Language header
        public async Task<string> SetLanguageFromBrowserAsync(string acceptLanguageHeader)
        {
            string defaultLanguage = "en"; // Fallback to English

            if (!string.IsNullOrEmpty(acceptLanguageHeader))
            {
                // Extract the first language code from the Accept-Language header
                var preferredLanguage = acceptLanguageHeader.Split(',').FirstOrDefault()?.Trim();

                // Check if the language exists in the database
                var language = await _languageRepository.GetByCodeAsync(preferredLanguage);

                if (language != null)
                {
                    defaultLanguage = preferredLanguage;
                }
            }

            // Set the detected or fallback language as the current language
            _currentLanguageCode = defaultLanguage;
            return defaultLanguage;
        }

        // Handle user-selected language
        public async Task<string> SetUserSelectedLanguageAsync(string languageCode)
        {
            // Check if the selected language exists in the database
            var language = await _languageRepository.GetByCodeAsync(languageCode);

            if (language != null)
            {
                _currentLanguageCode = languageCode; // Set the user-selected language
                return languageCode;
            }

            return "en"; // Fallback to English if the selected language is not found
        }

        public async Task<List<LanguageDto>> GetAllLanguagesAsync()
        {
            var languages = await _languageRepository.GetAllAsync();
            return languages.Select(l => new LanguageDto
            {
                Code = l.Code,
                Name = l.Name
            }).ToList();
        }
    } }
