using OrdersManagementSystem.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Services
{
	public interface IItemService
	{
		public Task<List<ItemDto>> GetItems();
		public Task<ItemDto> CreateOrUpdateItem(ItemDto model);
		public void DeleteItem(int id);
	}
}
