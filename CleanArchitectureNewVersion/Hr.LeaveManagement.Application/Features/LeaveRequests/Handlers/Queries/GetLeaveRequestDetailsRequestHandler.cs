using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistance;
using Hr.LeaveManagement.Application.DTOs.LeaveRequest;
using Hr.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailsRequestHandler : IRequestHandler<GetLeaverequestDetailsRequest, LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;

        public GetLeaveRequestDetailsRequestHandler(ILeaveRequestRepository leaveRequestRepository, IMapper mapper)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
        }
        public async Task<LeaveRequestDto> Handle(GetLeaverequestDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = await this.leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            return this.mapper.Map<LeaveRequestDto>(leaveRequest);
        }
    }
}
