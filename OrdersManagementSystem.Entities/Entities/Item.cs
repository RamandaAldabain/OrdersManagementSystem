using OrdersManagementSystem.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Entities.Entities
{
	public class Item : BaseEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public Double Price { get; set; }
		public int CategoryId { get; set; }
		public CategoryLookup Category { get; set; }
		public List<OrderItems> OrderItems { get; set; }

	}
}
