using Hr.LeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaverequestDetailsRequest : IRequest<LeaveRequestDto>
    {
        public int Id { get; set; }
    }
}
