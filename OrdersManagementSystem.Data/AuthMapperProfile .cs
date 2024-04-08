using AutoMapper;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Data
{
	public class AuthMapperProfile : Profile
	{
		public AuthMapperProfile()
		{
			CreateMap<RegisterRequestDto, User>().ReverseMap().ForMember(dest => dest.Role, opt => opt.Ignore());
			CreateMap<UserDto, User>().ReverseMap().ForMember(dest => dest.Role, opt => opt.Ignore());
			CreateMap<UserDto, RegisterRequestDto>().ReverseMap().ForMember(dest => dest.Role, opt => opt.Ignore());
			CreateMap<Item, ItemDto>().ReverseMap();
			CreateMap<Order, OrderDto>().ReverseMap();
		}
	}
}
