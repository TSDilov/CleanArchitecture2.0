using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistance;
using Hr.LeaveManagement.Application.DTOs;
using Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IMapper mapper;

        public GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.mapper = mapper;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await this.leaveTypeRepository.GetAll();
            return this.mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }
    }
}
