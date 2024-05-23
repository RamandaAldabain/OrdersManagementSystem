using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrdersManagementSystem.Data;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using OrdersManagementSystem.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Services
{
	public class ItemService : IItemService
	{
		private readonly DatabaseContext _DbContext;
		private readonly IMapper _mapper;

		public ItemService(DatabaseContext dbContext, IMapper mapper)
		{
			_DbContext = dbContext;
			_mapper = mapper;
		}
		public async Task<List<ItemDto>> GetItems()

		{
			var items = _DbContext.Items.ToList().Where(x => !x.IsDeleted);
            return _mapper.Map<List<ItemDto>>(items);
		}

		public async Task<ItemDto> CreateOrUpdateItem(ItemDto model)
		{
			var item = _mapper.Map<Item>(model);
			var category = _DbContext.CategoryLookup.Where(x => x.Id == model.CategoryId).FirstOrDefault();
			if(category == null) { throw new Exception("Category is Not Valid"); }
		

			if (model.Id == null || model.Id == 0)
			{
				await _DbContext.Items.AddAsync(item);

			}
			else
			{
				try { _DbContext.Items.Update(item); } catch (Exception ex) { }

			}
			

			await _DbContext.SaveChangesAsync();
			return _mapper.Map<ItemDto>(item);
		}

		public void DeleteItem(int id)
		{
			var itemExists = _DbContext.Items.Where(x=>x.Id == id).FirstOrDefault();
			if (itemExists == null) throw new Exception("Item does not Exists");
			_DbContext.Items.Remove(itemExists);
			_DbContext.SaveChanges();
		}
		public async Task<List<CategoryLookup>> GetCategories()
		{
			return await _DbContext.CategoryLookup.ToListAsync();
		}
	
	}
}
