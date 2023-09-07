using Hr.LeaveManagement.Application.Contracts.Persistance;
using Hr.LeaveManagement.Domain;

namespace Hr.LeaveManagement.Persistance.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly HrLeaveManagementDbContext dbContext;

        public LeaveTypeRepository(HrLeaveManagementDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
