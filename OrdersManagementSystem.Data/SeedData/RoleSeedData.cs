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
	public static class RoleSeedData
	{
		public static void RoleSeed(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<IdentityRole>().HasData(
				new IdentityRole
				{
					Name = "Admin",
					ConcurrencyStamp = "1",
					NormalizedName = "Admin"
				},
					new IdentityRole
					{
						Name = "User",
						ConcurrencyStamp = "2",
						NormalizedName = "User"
					}

				);

		}
	}

}
