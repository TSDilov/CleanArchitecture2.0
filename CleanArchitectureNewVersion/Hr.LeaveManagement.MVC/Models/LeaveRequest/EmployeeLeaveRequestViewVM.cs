using Hr.LeaveManagement.Domain;
using Hr.LeaveManagement.MVC.Models.LeaveAllocation;

namespace Hr.LeaveManagement.MVC.Models.LeaveRequest
{
    public class EmployeeLeaveRequestViewVM
    {
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
        public List<LeaveRequestVM> LeaveRequests { get; set; }
    }
}
