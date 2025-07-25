using AutoMapper;
using Foodz.API.DTOs.Auth;
using Foodz.API.DTOs.User;
using Foodz.API.DTOs.MenuItem;
using Foodz.API.DTOs.Order;
using Foodz.API.DTOs.DeliveryPersonnel;
using Foodz.API.Entitities;

namespace Foodz.API.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Define mappings here:
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<RegisterRequestDto, User>();

            CreateMap<MenuItem, MenuItemReadDto>();
            CreateMap<MenuItemCreateDto, MenuItem>();

            CreateMap<Order, OrderReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderCreateDto, Order>();

            CreateMap<OrderItem, OrderItemReadDto>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name))
                .ForMember(dest => dest.PriceAtOrderTime, opt => opt.MapFrom(src => src.MenuItem.Price));
            CreateMap<OrderItemCreateDto, OrderItem>();

            CreateMap<DeliveryPersonnel, DeliveryPersonnelReadDto>();
            CreateMap<DeliveryPersonnelCreateDto, DeliveryPersonnel>();
        }
    }

}