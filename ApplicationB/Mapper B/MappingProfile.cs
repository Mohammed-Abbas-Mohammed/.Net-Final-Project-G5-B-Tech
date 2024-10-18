using AutoMapper;
using DTOsB.Product;
using DTOsB.User;
using ModelsB.Authentication_and_Authorization_B;
using ModelsB.Product_B;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationB.Mapper_B
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductB, ProductDto>().ReverseMap(); 
            CreateMap<ProductImageB, ProductImageDto>().ReverseMap();
            CreateMap<ProductTranslationB, ProductTranslationDto>().ReverseMap();
            CreateMap<ProductSpecificationsB, ProductSpecificationDto>().ReverseMap();
            CreateMap<ProductSpecificationTranslationB, ProductSpecificationTranslationDto>().ReverseMap();
            CreateMap<SpecificationStore, SpecificationStoreDto>().ReverseMap();
            CreateMap<ReviewB, ReviewDto>().ReverseMap();
            CreateMap<ApplicationUserB, UserDto>().ReverseMap();


        }

    }
}
