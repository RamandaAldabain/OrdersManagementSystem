using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrdersManagementSystem.Data;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Services
{
	public class OrderService : IOrderService
	{
		private readonly DatabaseContext _DbContext;
		private readonly IMapper _mapper;

		public OrderService(DatabaseContext dbContext, IMapper mapper)
		{
			_DbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<OrderDto> AddToCart(ItemDto model, string userId)
		{
			var UserOrder = await GetUserActiveOrder(userId);
			if (UserOrder == null)
			{
				UserOrder = new OrderDto
				{
					userId = userId,
					isPlaced = false,
					OrderDate = DateTime.Now,
					items = new List<ItemDto>(),
					TotalPrice = model.Price
				};
				var mappedOrder = _mapper.Map<Order>(UserOrder);
				await _DbContext.AddAsync(mappedOrder);
				await _DbContext.SaveChangesAsync();

				var orderItem = new OrderItems
				{
					ItemId = (int)model.Id,
					OrderId = mappedOrder.Id
				};
				_DbContext.OrderItems.Add(orderItem);
				await _DbContext.SaveChangesAsync();
				return UserOrder;
			}
			else
			{
				var Order= _DbContext.Orders.Where(x=> x.Id == UserOrder.Id).FirstOrDefault();
				Order.TotalPrice += model.Price;
				_DbContext.Orders.Update(Order);
				var orderItem = new OrderItems
				{
					ItemId = (int)model.Id,
					OrderId = UserOrder.Id
				};
				await _DbContext.OrderItems.AddAsync(orderItem);
				await _DbContext.SaveChangesAsync();

				return _mapper.Map<OrderDto>(Order);
			}
			
		}
		public async Task<OrderDto> GetUserActiveOrder(string userId)
		{
			var order = await _DbContext.Orders.Where(x => x.UserId == userId && !x.isPlaced)
				.FirstOrDefaultAsync();
			return _mapper.Map<OrderDto>(order);

		}
		public async Task<List<OrderDto>> GetUserAllOrders(string userId)
		{
			var order = await _DbContext.Orders.Where(x => x.UserId == userId).ToListAsync();

			return _mapper.Map<List<OrderDto>>(order);

		}
		public async Task<OrderDto> PlaceOrder(int orderId) {

			var order = await _DbContext.Orders.Where(x => x.Id == orderId).FirstOrDefaultAsync();
			order.isPlaced = true;
			_DbContext.Update(order);
			await _DbContext.SaveChangesAsync();
			return _mapper.Map<OrderDto>(order);
		}
	}
}
