using Hr.LeaveManagement.Application.DTOs.LeaveRequest;
using Hr.LeaveManagement.Application.Responses;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class CreateLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}
