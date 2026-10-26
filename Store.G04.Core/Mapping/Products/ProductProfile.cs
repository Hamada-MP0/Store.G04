using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store.G04.Core.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Mapping.Products
{
    public class ProductProfile :Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Entity.Product, ProductDto>()
                .ForMember(d => d.BrandName, o =>  o.MapFrom(s => s.Brand == null ? string.Empty : s.Brand.Name))
                .ForMember(d => d.TypeName, o =>     o.MapFrom(s => s.Type == null ? string.Empty : s.Type.Name))
                //.ForMember(d => d.PictureUrl, options => options.MapFrom(s => $"{configuration["BaseUrl"]}{s.PictureUrl}"))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(new PictureURLResolver(configuration)))
                ;

            CreateMap<Entity.ProductBrand, TypeBrandDto>();
            CreateMap<Entity.ProductType, TypeBrandDto>();
        }
    }
}
