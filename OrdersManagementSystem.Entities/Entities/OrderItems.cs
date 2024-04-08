using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Entities.Entities
{
	public class OrderItems
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public Order Order { get; set; }

		public int ItemId { get; set; }
		public Item Item { get; set; }
	}
}
