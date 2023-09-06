using Hr.LeaveManagement.Application.DTOs.LeaveAllocation;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands
{
    public class UpdateLeaveAllocationCommand : IRequest<Unit>
    {
        public UpdateLeaveAllocationDto LeaveAllocationDto { get; set; }
    }
}
