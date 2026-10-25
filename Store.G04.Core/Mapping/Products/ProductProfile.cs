using AutoMapper;
using Store.G04.Core.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Mapping.Products
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Entity.Product, ProductDto>()
                .ForMember(d => d.BrandName, o => o.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.TypeName, o => o.MapFrom(s => s.Type.Name));
            CreateMap<Entity.ProductBrand, TypeBrandDto>();
            CreateMap<Entity.ProductType, TypeBrandDto>();
        }
    }
}
