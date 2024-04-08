using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OrdersManagementSystem.Data;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using OrdersManagementSystem.Services;

namespace OrdersManagementSystem.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	[Authorize]
	public class AccountController : ControllerBase
	{
		private readonly DatabaseContext _DbContext;
		private readonly JwtAuthenticationService _AuthService;
		private readonly UserService _UserService;
		public AccountController(DatabaseContext databaseContext, JwtAuthenticationService jwtAuthentication, UserService userService)
		{
			_DbContext = databaseContext;
			_AuthService = jwtAuthentication;
			_UserService = userService;
		}

		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var token = await _AuthService.AuthenticateAsync(model.Email, model.Password);

			if (token == null)
			{
				return Unauthorized("Invalid email or password.");
			}

			return Ok(new AuthResponseDto
			{
				Token = token,
				Result = true
			});
		}

		[HttpPost]
		[AllowAnonymous]

		public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
		{
			if (!model.Password.Equals(model.ConfirmPassword))
				return BadRequest("Password and confirm password do not match.");
			if (await _UserService.CheckIfEmailExist(model.Email) && (model.Id == "0" || model.Id == null))
				return Conflict("Email already exists.");
			if (await _UserService.CheckIfUserNameExist(model.UserName) && (model.Id == "0" || model.Id == null))
				return Conflict("UserName already exists.");


			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
		   	await _UserService.CreateUserAsync(model);
				return Ok("User added successfully.");
			}
			catch (Exception ex)
			{
				return StatusCode(500, "An unexpected error occurred, Please try again later.");
			}

		}

		[HttpDelete]
		public async Task DeleteUser(int id)
		{
			await _UserService.DeleteUser(id);

		}

		[HttpGet]

		public List<User> GetAllUsers()
		{
			return _UserService.GetAllUsers();
		}
	}

}
