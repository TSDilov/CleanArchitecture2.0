using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.Exceptions;
using Hr.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Hr.LeaveManagement.Domain;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await this.leaveAllocationRepository.Get(request.Id);
            if (leaveAllocation == null)
            {
                throw new NotFoundException(nameof(Hr.LeaveManagement.Domain.LeaveAllocation), request.Id);
            }
            await this.leaveAllocationRepository.Delete(leaveAllocation);
            return Unit.Value;
        }
    }
}
