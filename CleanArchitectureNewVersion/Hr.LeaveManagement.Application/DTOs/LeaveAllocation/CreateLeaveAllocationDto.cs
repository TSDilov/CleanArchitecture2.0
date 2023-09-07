﻿namespace Hr.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class CreateLeaveAllocationDto : ILeaveAllocationDto
    {
        public int NumberDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
