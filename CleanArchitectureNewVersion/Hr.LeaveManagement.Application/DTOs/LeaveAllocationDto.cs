using Hr.LeaveManagement.Application.DTOs.Common;

namespace Hr.LeaveManagement.Application.DTOs
{
    public class LeaveAllocationDto : BaseDto
    {
        public int NumberDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
