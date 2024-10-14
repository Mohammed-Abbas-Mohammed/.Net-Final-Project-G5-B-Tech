using AutoMapper;
using DTOsB.Category;
using ModelsB.Category_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationB.Mapper_B
{
    public class CategoryMappingProfile: Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryB, GetAllCategoriesDTO>();
            CreateMap<CategoryTranslationB, CategoryTranslationDTO>();

            CreateMap<CreateOrUpdateCategoriesDTO, CategoryB>();
            CreateMap<CreateCategoryTranslationDto, CategoryTranslationB>();
        }
    }
}
