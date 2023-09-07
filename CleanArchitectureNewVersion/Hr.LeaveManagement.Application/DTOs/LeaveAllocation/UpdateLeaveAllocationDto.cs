using Hr.LeaveManagement.Application.DTOs.Common;

namespace Hr.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class UpdateLeaveAllocationDto : BaseDto, ILeaveAllocationDto
    {
        public int NumberDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
