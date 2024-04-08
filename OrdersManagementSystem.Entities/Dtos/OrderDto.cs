using OrdersManagementSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Entities.Dtos
{
	public class OrderDto
	{
		public int Id { get; set; }
		public string userId { get; set; }
		public bool isPlaced { get; set; }
		public DateTime OrderDate { get; set; }
		public double TotalPrice { get; set; }

		public List<ItemDto> items { get; set; }

	
	}
}
