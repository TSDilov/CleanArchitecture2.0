namespace Hr.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public interface ILeaveAllocationDto
    {
        public int NumberDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
