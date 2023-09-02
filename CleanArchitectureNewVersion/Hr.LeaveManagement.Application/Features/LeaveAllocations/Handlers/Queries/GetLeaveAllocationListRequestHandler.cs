using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistance;
using Hr.LeaveManagement.Application.DTOs;
using Hr.LeaveManagement.Application.Features.LeaveAllocation.Requests.Queries;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocation.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationsListRequest, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;

        public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
        }
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await this.leaveAllocationRepository.GetLeaveAllocationsWithDetails();
            return this.mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
        }
    }
}
