using Hr.LeaveManagement.Application.Models.Identity;

namespace Hr.LeaveManagement.Application.Contracts.Identity
{
    public interface IUserService
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(string userId);
    }
}
