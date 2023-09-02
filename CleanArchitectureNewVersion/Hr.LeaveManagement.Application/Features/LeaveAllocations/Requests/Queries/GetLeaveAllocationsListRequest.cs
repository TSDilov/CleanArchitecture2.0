using Hr.LeaveManagement.Application.DTOs;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries
{
    public class GetLeaveAllocationsListRequest : IRequest<List<LeaveAllocationDto>>
    {
    }
}
