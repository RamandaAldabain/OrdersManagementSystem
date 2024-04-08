using Microsoft.AspNetCore.Identity.Data;
using OrdersManagementSystem.Entities.Dtos;
using OrdersManagementSystem.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OrdersManagementSystem.Services
{
    public interface IUserService
    {
		public Task<bool> CreateUserAsync(RegisterRequestDto model);
		public  Task DeleteUser(int id);
	}
}
