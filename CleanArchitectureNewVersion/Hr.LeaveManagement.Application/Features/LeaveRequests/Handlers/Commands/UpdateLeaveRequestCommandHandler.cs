using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using Hr.LeaveManagement.Application.Exceptions;
using Hr.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, 
            IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {           
            var leaveRequest = await this.leaveRequestRepository.Get(request.Id);
            if (request.LeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(this.leaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult);
                }

                this.mapper.Map(request.LeaveRequestDto, leaveRequest);
                await this.leaveRequestRepository.Update(leaveRequest);
            }
            else if (request.ChangeLeaveRequestApprovalDto != null) 
            {
                await this.leaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocation = await this.leaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                    var daysRequired = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;
                    allocation.NumberDays = daysRequired;
                    await this.leaveAllocationRepository.Update(allocation);
                }
            }
            
            return Unit.Value;
        }
    }
}
