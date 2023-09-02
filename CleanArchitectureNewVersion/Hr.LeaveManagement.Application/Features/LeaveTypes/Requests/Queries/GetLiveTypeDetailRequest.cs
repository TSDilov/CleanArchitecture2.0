using Hr.LeaveManagement.Application.DTOs;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries
{
    public class GetLiveTypeDetailRequest : IRequest<LeaveTypeDto>
    {
        public int Id { get; set; }
    }
}
