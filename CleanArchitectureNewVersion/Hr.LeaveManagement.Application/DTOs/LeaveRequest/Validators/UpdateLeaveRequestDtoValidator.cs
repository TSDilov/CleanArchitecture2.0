using FluentValidation;
using Hr.LeaveManagement.Application.Contracts.Persistence;

namespace Hr.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            Include(new ILeaveRequestDtoValidator(this.leaveTypeRepository));

            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} must be present.");
        }
    }
}
