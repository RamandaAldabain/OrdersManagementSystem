using OrdersManagementSystem.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Entities.Dtos
{
	public class RegisterRequestDto
	{
		public string? Id { get; set; }
		[Required(ErrorMessage = "FirstNameIsRequired")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "userNameIsRequired")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "LastNameIsRequired")]
		public string LastName { get; set; }

		[Required(ErrorMessage = "EmailIsRequired")]

		[RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "PasswordIsRequired")]
		public string Password { get; set; }

		[Required(ErrorMessage = "ConfirmPasswordIsRequired")]
		public string ConfirmPassword { get; set; }

		[Required(ErrorMessage = "RoleIsRequired")]
		public string Role { get; set; }

	}
}
