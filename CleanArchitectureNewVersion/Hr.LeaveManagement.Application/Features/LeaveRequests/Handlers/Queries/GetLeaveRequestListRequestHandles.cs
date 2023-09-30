using AutoMapper;
using Hr.LeaveManagement.Application.Constants;
using Hr.LeaveManagement.Application.Contracts.Identity;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.DTOs.LeaveRequest;
using Hr.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using Hr.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hr.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, List<LeaveRequestListDto>>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;

        public GetLeaveRequestListRequestHandler(ILeaveRequestRepository leaveRequestRepository, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }

        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();
            if (request.IsLoggedInUser)
            {
                var userId = this.httpContextAccessor.HttpContext.User.FindFirst(
                    u => u.Type == CustomClaimTypes.Uid)?.Value;
                leaveRequests = await this.leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

                var employee = await this.userService.GetEmployee(userId);
                requests = this.mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = employee;
                }
            }
            else
            {
                leaveRequests = await this.leaveRequestRepository.GetLeaveRequestsWithDetails();
                requests = this.mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                foreach (var req in requests)
                {
                    req.Employee = await this.userService.GetEmployee(req.RequestingEmployeeId);
                }
            }

            return requests;
        }
    }
}
