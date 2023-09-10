using Hr.LeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands
{
    public class UpdateLeaveTypeCommand : IRequest<Unit>
    {
        public LeaveTypeDto LeaveTypeDto { get; set; }
    }
}
