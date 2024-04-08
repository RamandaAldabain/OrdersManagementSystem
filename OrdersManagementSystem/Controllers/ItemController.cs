using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersManagementSystem.Data;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Lookups;
using OrdersManagementSystem.Services;
using System.Web.Http.Cors;

namespace OrdersManagementSystem.Controllers
{

	[Route("api/[controller]/[action]")]
	[ApiController]

	public class ItemController : ControllerBase
	{
		private readonly ItemService _ItemService;

		public ItemController(ItemService itemService)
		{
			_ItemService = itemService;
		}
		[HttpGet]

		public async Task<List<ItemDto>> GetAllItems()
		
		{
			return await _ItemService.GetItems();
		}
		[HttpGet]
		public async Task<List<CategoryLookup>> GetCategories()
		{
			return await _ItemService.GetCategories();
		}

		[HttpPost]
		public async Task<ItemDto> CreateOrUpdateItem(ItemDto model)
		{
			if (!ModelState.IsValid) throw new Exception("Model is not Valid");
			return await _ItemService.CreateOrUpdateItem(model);
		}
		[HttpDelete]
		public void DeleteItem(int id)
		{
			if(id== null || id == 0) { throw new Exception("invalid id"); }
			_ItemService.DeleteItem(id);

		}


	}
}
