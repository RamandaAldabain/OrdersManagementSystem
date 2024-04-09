using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using OrdersManagementSystem.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Data.SeedData
{
	public static class ItemsSeedData
	{
		public static void ItemsSeed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Item>().HasData(
				new Item
				{
					Id = 1,
					Name = "Item",
					Description= "Description",
					Price=100,
					CategoryId= 2,
				}, new Item
				{
					Id = 2,
					Name = "Item",
					Description = "Description",
					Price = 100,
					CategoryId = 2,
				},
				new Item
				{
					Id = 3,
					Name = "Item 3",
					Description = "Description",
					Price = 100,
					CategoryId = 3,
				}
				
			);
		}
	}
}
