using Hr.LeaveManagement.Domain;
using Hr.LeaveManagement.MVC.Models.LeaveRequest;
using Hr.LeaveManagement.MVC.Services.Base;

namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface ILeaveRequestService
    {
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestList();
        Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();
        Task<Response<int>> CreateLeaveRequest(CreateLeaveRequestVM leaveRequest);
        Task<LeaveRequestVM> GetLeaveRequest(int id);
        Task DeleteLeaveRequest(int id);
        Task ApproveLeaveRequest(int id, bool approved);
    }
}
