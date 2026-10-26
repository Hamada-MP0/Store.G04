using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Mapping.Products
{
    public class PictureURLResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.PictureUrl}";
            }
            return null;
        }
    }
}
