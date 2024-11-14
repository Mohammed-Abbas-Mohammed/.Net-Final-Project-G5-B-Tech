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
        private readonly IProductTranslationService _productTranslationService;
        private int _currentLanguageId;

        public LanguageService(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
            _currentLanguageId = 2; // Default to English
        }

        

        public async Task<ResultView<LanguageDto>> CreateProductAsync(LanguageDto langDto)
        {
            if (langDto.Id > 0)
                return ResultView<LanguageDto>.Failure("Language already exists. Use update to modify it.");

            var lang = _mapper.Map<LanguageB>(langDto);
            var createdLang = await _languageRepository.AddAsync(lang);

            return ResultView<LanguageDto>.Success(_mapper.Map<LanguageDto>(createdLang));
        }


        public async Task<ResultView<LanguageDto>> UpdateProductAsync(LanguageDto langDto)
        {
            if (langDto == null)
                return ResultView<LanguageDto>.Failure("Language must have data to be added");

            var existingLang = await _languageRepository.GetByIdAsync(langDto.Id);
            if (existingLang == null )
                return ResultView<LanguageDto>.Failure("Language not found. Unable to update.");
                
            _mapper.Map(langDto, existingLang);
            
            await _languageRepository.UpdateAsync(existingLang);
            return ResultView<LanguageDto>.Success(langDto);
        }
        

         public async Task<ResultView<LanguageDto>> DeleteProductAsync(int id)
        {
            var existingLang = await _languageRepository.GetByIdAsync(id);
            
            if (existingLang == null)
                return ResultView<LanguageDto>.Failure("Language not found. Unable to delete.");

            var product = await _productTranslationService.GetAllTranslationsAsync().FirstOrDefault(l=>l.language==id);
            if(product != null)
                 return ResultView<LanguageDto>.Failure("Language is related with product. Unable to delete.");
                 
            await _languageRepository.UpdateAsync(existingLang);
            return ResultView<LanguageDto>.Success(null);
        }

        // Get the currently selected language
        public int GetCurrentLanguageCode()
        {
            return _currentLanguageId;
        }

        // Set language manually (user selection)
        public void SetCurrentLanguageCode(int LanguageId)
        {
            if (LanguageId == null)
            {
                throw new ArgumentException("Invalid language code");
            }

            _currentLanguageId = LanguageId;
        }

        public void SetCurrentLanguageCode(string LanguageCode)
        {
            if (LanguageCode == null)
            {
                throw new ArgumentException("Invalid language code");
            }


            _currentLanguageId = LanguageCode.Contains("ar") ? 1 : 2;


        }




        // Handle default language detection from Accept-Language header
        public async Task<string> SetLanguageFromBrowserAsync(string acceptLanguageHeader)
        {
            string defaultLanguageCode = "en"; // Fallback to a default language ID, e.g., English

            if (!string.IsNullOrEmpty(acceptLanguageHeader))
            {
                // Extract the first language code from the Accept-Language header
                var preferredLanguageCode = acceptLanguageHeader.Split(',').FirstOrDefault()?.Trim();

                if (!string.IsNullOrEmpty(preferredLanguageCode))
                {
                    // Check if the language code exists in the database
                    var language = await _languageRepository.GetByCodeAsync(preferredLanguageCode);

                    // If the language is found, return its ID
                    if (language != null)
                    {
                        return language.Code;
                    }
                }
            }

            // Set the fallback language ID if no matching language was found
            return defaultLanguageCode;
        }


        // Handle user-selected language
        public async Task<int> SetUserSelectedLanguageAsync(int languageId)
        {
            // Check if the selected language exists in the database
            var language = await _languageRepository.GetByIdAsync(languageId);

            if (language != null)
            {
                _currentLanguageId = languageId; // Set the user-selected language
                return languageId;
            }

            return 2; // Fallback to English if the selected language is not found
        }

        public async Task<List<LanguageDto>> GetAllLanguagesAsync()
        {
            var languages = await _languageRepository.GetAllAsync();
            return languages.Select(l => new LanguageDto
            {
                Code = l.Code,
                Id = l.Id,
                Name = l.Name,
            }).ToList();
        }
    } }
