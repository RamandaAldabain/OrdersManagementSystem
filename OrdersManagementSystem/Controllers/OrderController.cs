using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using OrdersManagementSystem.Services;

namespace OrdersManagementSystem.Api.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class OrderController : ControllerBase {


		private readonly OrderService _OrderService;

		public OrderController(OrderService orderService)
		{
			_OrderService = orderService;
		}

		[HttpGet]
		public async Task<OrderDto> GetUserActiveOrder(string userId)
		{
			return await _OrderService.GetUserActiveOrder(userId);
		}
		
		[HttpGet]
		public async Task<List<OrderDto>> GetUserAllOrders(string userId)
		{
			return await _OrderService.GetUserAllOrders(userId);
		}
		[HttpPost]
		public async Task<OrderDto> PlaceOrder(int orderId)
		{
			return await _OrderService.PlaceOrder(orderId);
		}
		[HttpPost]
		public async Task<OrderDto> AddToCart(ItemDto model, string userId)
		{
			return await _OrderService.AddToCart(model,userId);

		}
	}

}
