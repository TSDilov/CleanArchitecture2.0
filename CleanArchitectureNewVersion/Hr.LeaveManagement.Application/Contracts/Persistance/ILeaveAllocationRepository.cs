using Hr.LeaveManagement.Domain;

namespace Hr.LeaveManagement.Application.Contracts.Persistance
{
    public interface ILeaveAllocationRepository: IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
    }
}
