using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Entities.Dtos
{
	public class LoginRequestDto
	{
		[Required(ErrorMessage = "EmailIsRequired")]
		public string Email { get; set; }
		[Required(ErrorMessage = "PasswordIsRequired")]
		public string Password { get; set; }
	}
}
