using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using OrdersManagementSystem.Data;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using OrdersManagementSystem.Entities.Lookups;
using System.Data;

namespace OrdersManagementSystem.Services
{
	public class UserService : IUserService
	{
		private readonly DatabaseContext _DbContext;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserService(DatabaseContext databaseContext, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			_DbContext = databaseContext;
			_mapper = mapper;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public List<User> GetAllUsers()
		{
			var users = _DbContext.Users.ToList();
							
			if (users == null) throw new Exception("Users Not Found");
			return users;
		}
		public User GetUser(string Email, string Password)
		{
			var user = _DbContext.Users
							.Where(i => i.Email == Email && i.Password == Password).FirstOrDefault();
			if (user == null) throw new Exception("Wrong Email or Password");
			return user;
		}
		public async Task<string> GetUserRole(User user)
		{
			var roles = await _userManager.GetRolesAsync(user);
			return roles.Count > 0 ? roles[0] : "";
		}
		public async Task<bool> CheckIfEmailExist(string email)
		{
			if (string.IsNullOrWhiteSpace(email))
			{
				throw new Exception("Email cannot be null or empty.");
			}
			var userExists = _DbContext.Users.Any(x => x.Email == email);

			return userExists;
		}
		public async Task<bool> CheckIfUserNameExist(string userName)
		{
			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new Exception("userName cannot be null or empty.");
			}
			var userExists = _DbContext.Users.Any(x => x.UserName == userName);

			return userExists;
		}

		public async Task<bool> CreateUserAsync(RegisterRequestDto model)
		{
			var existingUser = await _userManager.FindByEmailAsync(model.Email);
			if (existingUser != null)
			{
				return false;
			}

			var user = _mapper.Map<User>(model);
			user.Id= Guid.NewGuid().ToString();
			var result = await _userManager.CreateAsync(user, model.Password);

			if (result.Succeeded)
			{
				if (!await _roleManager.RoleExistsAsync(model.Role))
				{
					await _roleManager.CreateAsync(new RoleLookup { Name = model.Role });
				}
				await _userManager.AddToRoleAsync(user, model.Role);

				return true;
			}
			
			return false;
		}


		public async Task DeleteUser(int id)
		{
			try
			{
				var userToDelete = _DbContext.Users.Find(id);

				if (userToDelete == null)
				{
					throw new ArgumentException($"User with ID '{id}' not found.", nameof(id));
				}

				await _userManager.DeleteAsync(userToDelete);
			}
			catch (ArgumentException ex)
			{
				throw new Exception($"Failed to delete user with ID {id}.", ex);
			}
		}
	}
}
