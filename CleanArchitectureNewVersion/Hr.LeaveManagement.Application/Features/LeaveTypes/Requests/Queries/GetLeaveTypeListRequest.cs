using Hr.LeaveManagement.Application.DTOs;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLeaveTypeListRequest : IRequest<List<LeaveTypeDto>>
    {
    }
}
