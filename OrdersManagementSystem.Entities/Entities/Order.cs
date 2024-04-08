using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Entities.Entities
{
	public class Order : BaseEntity
	{
		public DateTime OrderDate { get; set; }
		public Double TotalPrice { get; set; }
		public bool isPlaced { get; set; }


		public string UserId { get; set; }
		public User User { get; set; }
		public List<OrderItems> OrderItems { get; set; }
	}
}
