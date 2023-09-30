using Hr.LeaveManagement.Application.DTOs.LeaveRequest;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestListRequest : IRequest<List<LeaveRequestListDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
