using AutoMapper;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Mapping.Auth
{
    public class AuthenticationProfile :Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }

    }
}
