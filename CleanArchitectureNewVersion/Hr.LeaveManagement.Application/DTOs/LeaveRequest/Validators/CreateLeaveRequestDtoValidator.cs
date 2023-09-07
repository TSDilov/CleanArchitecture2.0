using FluentValidation;
using Hr.LeaveManagement.Application.Contracts.Persistence;

namespace Hr.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveRequestDtoValidator(this.leaveTypeRepository));
            
        }
    }
}
