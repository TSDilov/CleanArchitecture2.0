using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistance;
using Hr.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using Hr.LeaveManagement.Application.Exceptions;
using Hr.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationDtoValidator(this.leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult);
            }

            var leaveAllocation = await this.leaveAllocationRepository.Get(request.LeaveAllocationDto.Id);
            this.mapper.Map(request.LeaveAllocationDto, leaveAllocation);
            await this.leaveAllocationRepository.Update(leaveAllocation);
            return Unit.Value;
        }
    }
}
