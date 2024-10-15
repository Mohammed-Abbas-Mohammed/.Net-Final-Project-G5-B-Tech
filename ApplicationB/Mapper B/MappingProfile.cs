﻿using AutoMapper;
using DTOsB.Product;
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
        }
    
    }
}