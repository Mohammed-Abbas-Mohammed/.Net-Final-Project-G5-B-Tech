﻿using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using DTOsB.Category;
using DTOsB.Product;
using DTOsB.Shared;
using Microsoft.AspNetCore.Http;
using ModelsB.Category_B;
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



            // ProductB to ProductDto mapping
            CreateMap<ProductB, ProductDto>()
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => src.Specifications))
                .ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Translations))
                .ReverseMap();
            CreateMap<ProductB, ProductCreateOrUpdateDto>()
                //.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))

                .ReverseMap();
            CreateMap<ProductImageB, ProductImageDto>().ReverseMap();
            CreateMap<ProductImageB,ProductImageCreateOrUpdateDto >()
            //.ForMember(dest => dest.Url, opt => opt.Ignore()) // We can ignore URL during mapping
            .ForMember(dest => dest.ImageFile, opt => opt.Ignore()).ReverseMap();

            // Mapping for product translations
            CreateMap<ProductTranslationB, ProductTranslationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.LanguageId, opt => opt.MapFrom(src => src.LanguageId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ReverseMap();


            CreateMap<ProductSpecificationsB, ProductSpecificationDto>().ReverseMap();
            CreateMap<ProductSpecificationTranslationB, ProductSpecificationTranslationDto>().ReverseMap();
            CreateMap<SpecificationStore, SpecificationStoreDto>().ReverseMap();
            CreateMap<ReviewB, ReviewDto>().ReverseMap();

     //       CreateMap<ProductB, ProductViewModel>()
     ////.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))  // Assuming 'Id' in ProductB corresponds to 'ProductId'
     ////.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
     ////.ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => src.Specifications))
     ////.ForMember(dest => dest.Translations, opt => opt.MapFrom(src => src.Translations))
     ////.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))  // Assuming 'Price' exists in ProductB
     ////.ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))  // Assuming 'StockQuantity' exists in ProductB
     ////.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))  // Assuming 'CreatedBy' exists in ProductB
     ////.ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created))  // Assuming 'Created' exists in ProductB
     ////.ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy))  // Assuming 'UpdatedBy' exists in ProductB
     ////.ForMember(dest => dest.Updated, opt => opt.MapFrom(src => src.Updated))  // Assuming 'Updated' exists in ProductB
     ////.ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))  // Assuming 'IsDeleted' exists in ProductB
     //.ReverseMap();
            CreateMap<ProductTranslationB, ProductTranslationViewModel>().ReverseMap();
            CreateMap<ProductSpecificationsB, ProductSpecificationViewModel>().ReverseMap();
            CreateMap<ProductSpecificationTranslationB, ProductSpecificationTranslationViewModel>()
             .ForMember(dest => dest.TranslatedKey, opt => opt.MapFrom(src => src.TranslatedKey))
             .ForMember(dest => dest.TranslatedValue, opt => opt.MapFrom(src => src.TranslatedValue)).ReverseMap();

            CreateMap<ProductDto, ProductViewModelWithLang>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.AvailableLanguages, opt => opt.Ignore()) // Set this in the controller
                .ForMember(dest => dest.SelectedLanguageId, opt => opt.Ignore())
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => new List<ProductDto> { src })); 



            //CreateMap<CategoryB, GetAllCategoriesDTO>().ReverseMap();
            //CreateMap<CategoryB, CreateOrUpdateCategoriesDTO>().ReverseMap();
            //CreateMap<CategoryTranslationB, CategoryTranslationDTO>().ReverseMap();
            //CreateMap<CategoryTranslationB, CreateCategoryTranslationDto>().ReverseMap();
            CreateMap<LanguageDto, LanguageDto>();

        }

      

    }
}