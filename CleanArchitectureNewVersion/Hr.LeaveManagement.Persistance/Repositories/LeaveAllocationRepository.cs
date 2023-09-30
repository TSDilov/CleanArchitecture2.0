using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hr.LeaveManagement.Persistance.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly HrLeaveManagementDbContext dbContext;

        public LeaveAllocationRepository(HrLeaveManagementDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await this.dbContext.AddRangeAsync(allocations);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await this.dbContext.LeaveAllocations.AnyAsync(x => x.EmployeeId == userId
                                            && x.LeaveTypeId == leaveTypeId
                                            && x.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            return await this.dbContext.LeaveAllocations
                .Include(x => x.LeaveType)
                .ToListAsync();
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            return await this.dbContext.LeaveAllocations.Where(a => a.EmployeeId == userId)
                .Include(x => x.LeaveType)
                .ToListAsync();
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return await this.dbContext.LeaveAllocations
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await this.dbContext.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == userId
                                        && q.LeaveTypeId == leaveTypeId);
        }
    }
}
