using AutoMapper;
using Store.G04.Core.Dtos.Baskets;
using Store.G04.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Mapping.Basket
{
    public class BasketProfile : Profile
    {
       public BasketProfile() {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        }
    }
}
