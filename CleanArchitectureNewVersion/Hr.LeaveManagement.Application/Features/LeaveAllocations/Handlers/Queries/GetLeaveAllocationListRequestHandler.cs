using AutoMapper;
using Hr.LeaveManagement.Application.Constants;
using Hr.LeaveManagement.Application.Contracts.Identity;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.DTOs.LeaveAllocation;
using Hr.LeaveManagement.Application.DTOs.LeaveRequest;
using Hr.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using Hr.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationsListRequest, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserService userService;

        public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUserService userService)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = new List<Hr.LeaveManagement.Domain.LeaveAllocation>();
            var allocations = new List<LeaveAllocationDto>();
            if (request.IsLoggedInUser)
            {
                var userId = this.httpContextAccessor.HttpContext.User.FindFirst(
                    u => u.Type == CustomClaimTypes.Uid)?.Value;
                leaveAllocations = await this.leaveAllocationRepository.GetLeaveAllocationsWithDetails(userId);

                var employee = await this.userService.GetEmployee(userId);
                allocations = this.mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach (var all in allocations)
                {
                    all.Employee = employee;
                }
            }
            else
            {
                leaveAllocations = await this.leaveAllocationRepository.GetLeaveAllocationsWithDetails();
                allocations = this.mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
                foreach (var all in allocations)
                {
                    all.Employee = await this.userService.GetEmployee(all.EmployeeId);
                }
            }

            return allocations;
        }
    }
}
