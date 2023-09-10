using Hr.LeaveManagement.Application.DTOs.Common;
using Hr.LeaveManagement.Application.DTOs.LeaveType;

namespace Hr.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class LeaveRequestDto : BaseDto, ILeaveRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveTypeDto Leavetype { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime DateRequested { get; set; }
        public string RequestComments { get; set; }
        public DateTime? DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Canceled { get; set; }
    }
}
