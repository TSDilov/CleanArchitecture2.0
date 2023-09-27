﻿using Hr.LeaveManagement.MVC.Services.Base;

namespace Hr.LeaveManagement.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}
