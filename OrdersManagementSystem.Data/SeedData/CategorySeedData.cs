using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdersManagementSystem.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Data.SeedData
{
	public static class CategorySeedData
	{
		public static void CategorySeed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CategoryLookup>().HasData(
				new CategoryLookup
				{
					Id = 1,
					Name = "Electronics"
				},
				new CategoryLookup
				{
					Id = 2,
					Name = "Clothing",
				},
				new CategoryLookup
				{
					Id = 3,
					Name = "Books",
				},
				new CategoryLookup
				{
					Id = 4,
					Name = "Furniture",
				},
				new CategoryLookup
				{
					Id = 5,
					Name = "Food",
				},
				new CategoryLookup
				{
					Id = 6,
					Name = "Other",
				}
			);
		}
	}
}
