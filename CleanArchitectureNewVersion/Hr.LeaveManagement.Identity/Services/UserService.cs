using Hr.LeaveManagement.Application.Contracts.Identity;
using Hr.LeaveManagement.Application.Models.Identity;
using Hr.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Hr.LeaveManagement.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<Employee> GetEmployee(string userId)
        {
            var employee = await userManager.FindByIdAsync(userId);
            return new Employee
            {
                Email = employee.Email,
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var employees = await this.userManager.GetUsersInRoleAsync("Employee");
            return employees.Select(x => new Employee
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
            }).ToList();
        }
    }
}
