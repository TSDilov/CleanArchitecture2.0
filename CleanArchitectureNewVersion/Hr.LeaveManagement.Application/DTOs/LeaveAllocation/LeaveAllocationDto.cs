using Hr.LeaveManagement.Application.DTOs.Common;
using Hr.LeaveManagement.Application.DTOs.LeaveType;
using Hr.LeaveManagement.Application.Models.Identity;

namespace Hr.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class LeaveAllocationDto : BaseDto
    {
        public int NumberDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public Employee Employee { get; set; }
        public string EmployeeId { get; set; }
        public int Period { get; set; }
    }
}
