using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.DTOs.LeaveType;
using Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeDetailsRequestHandler : IRequestHandler<GetLiveTypeDetailRequest, LeaveTypeDto>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;

        public GetLeaveTypeDetailsRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
        }
        public async Task<LeaveTypeDto> Handle(GetLiveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await this.leaveTypeRepository.Get(request.Id);
            return this.mapper.Map<LeaveTypeDto>(leaveType);
        }
    }
}
