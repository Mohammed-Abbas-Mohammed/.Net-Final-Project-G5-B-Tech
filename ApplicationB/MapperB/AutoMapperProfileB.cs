using AutoMapper;
using DTOsB.Product;
using ModelsB.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationB.MapperMAFP
{
    public class AutoMapperProfileB : Profile
    {
        public AutoMapperProfileB()
        {

            //product Mapping
            CreateMap<ProductB, GetProductDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Categories.FirstOrDefault().CategoryName)).ReverseMap();

            CreateMap<ProductB,CreateOrUpdateProductDTO>().ReverseMap();
        }
    }
}
