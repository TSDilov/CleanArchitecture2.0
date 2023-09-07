using Hr.LeaveManagement.Application.Contracts.Persistance;
using Hr.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace Hr.LeaveManagement.Persistance.Repositories
{
    public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly HrLeaveManagementDbContext dbContext;

        public LeaveRequestRepository(HrLeaveManagementDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task ChangeApprovalStatus(LeaveRequest leaveRequest, bool? ApprovalStatus)
        {
            leaveRequest.Approved = ApprovalStatus;
            this.dbContext.Entry(leaveRequest).State = EntityState.Modified;
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsWithDetails()
        {
            return await this.dbContext.LeaveRequests
                .Include(x => x.LeaveType)
                .ToListAsync();            
        }

        public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
        {
            return await this.dbContext.LeaveRequests
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
