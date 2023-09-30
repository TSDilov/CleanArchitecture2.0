using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Identity;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.DTOs.LeaveRequest;
using Hr.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestDetailsRequestHandler : IRequestHandler<GetLeaveRequestDetailsRequest, LeaveRequestDto>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IUserService userService;

        public GetLeaveRequestDetailsRequestHandler(ILeaveRequestRepository leaveRequestRepository, 
            IMapper mapper,
            IUserService userService)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.userService = userService;
        }
        public async Task<LeaveRequestDto> Handle(GetLeaveRequestDetailsRequest request, CancellationToken cancellationToken)
        {
            var leaveRequest = this.mapper.Map<LeaveRequestDto>(await this.leaveRequestRepository.GetLeaveRequestWithDetails(request.Id));
            leaveRequest.Employee = await this.userService.GetEmployee(leaveRequest.RequestingEmployeeId);
            return leaveRequest;
        }
    }
}
