
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Dtos.Orders;
using Store.G04.Core.Entity.Order;

namespace Store.G04.Core.Mapping.Orders
{
    public class OrderProfile : Profile
    {
        public OrderProfile(IConfiguration configuration)
        {
            // Address mapping
            CreateMap<AddressDto, Address>()
                .ConstructUsing(src =>
                    new Address(
                        src.LName,
                        src.Fname,
                        src.City,
                        src.Country,
                        src.Street));

            CreateMap<Address, AddressDto>()
                .ForMember(d => d.Fname,
                    options => options.MapFrom(s => s.FirstName))
                .ForMember(d => d.LName,
                    options => options.MapFrom(s => s.LastName));

            // Order mapping
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod,
                    options => options.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost,
                    options => options.MapFrom(s => s.DeliveryMethod.Cost.ToString()))
                .ForMember(d => d.Total,
                    options => options.MapFrom(s => s.SubTotal + s.DeliveryMethod.Cost))
                .ForMember(d => d.Status,
                    options => options.MapFrom(s => s.Status.ToString()));

            // Order items mapping
            CreateMap<OrderItems, OrderItemDto>()
                .ForMember(d => d.ProductId,
                    options => options.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName,
                    options => options.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl,
                    options => options.MapFrom(s =>
                        $"{configuration["BaseUrl"]}{s.Product.PictureURL}"));
        }
    }
}
