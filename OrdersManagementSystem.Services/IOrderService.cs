using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Services
{
	public interface IOrderService
	{
		public Task<OrderDto> PlaceOrder(int orderId);
		public Task<OrderDto> GetUserActiveOrder(string userId);
		public Task<List<OrderDto>> GetUserAllOrders(string userId);
		public Task<OrderDto> AddToCart(ItemDto model, string userId);
	}
}
