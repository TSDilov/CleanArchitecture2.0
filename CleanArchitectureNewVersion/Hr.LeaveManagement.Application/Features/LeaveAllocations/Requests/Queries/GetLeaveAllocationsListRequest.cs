using Hr.LeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries
{
    public class GetLeaveAllocationsListRequest : IRequest<List<LeaveAllocationDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
