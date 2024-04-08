using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OrdersManagementSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Services
{
	public class JwtAuthenticationService
	{
		private readonly string _key;
		private readonly UserService _userService;

		public JwtAuthenticationService(string key, UserService userService)
		{
			_key = key;
			_userService = userService;

		}

		public async Task<string> AuthenticateAsync(string email, string password)
		{ var user = _userService.GetUser(email, password);
			if(user == null) { throw new Exception("User Not found"); }
			var role = await _userService.GetUserRole(user);

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.ASCII.GetBytes(_key);
			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
			new Claim(ClaimTypes.Email, email),
			new Claim(ClaimTypes.Role, role),

				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(tokenKey),
					SecurityAlgorithms.HmacSha256Signature)
			};
		
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}
}
