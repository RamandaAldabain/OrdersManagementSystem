using OrdersManagementSystem.Entities.Entities;
using OrdersManagementSystem.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Entities.Dtos
{
	public class ItemDto 
	{
		public int? Id { get; set; }
		[Required(ErrorMessage = "NameIsRequired")]
		public string Name { get; set; }
		[Required(ErrorMessage = "DescriptionIsRequired")]
		public string Description { get; set; }
		[Required(ErrorMessage = "PriceIsRequired")]
		[Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
		public Double Price { get; set; }
		[Required(ErrorMessage = "CategoryIsRequired")]
		public int CategoryId { get; set; }

	}
}
