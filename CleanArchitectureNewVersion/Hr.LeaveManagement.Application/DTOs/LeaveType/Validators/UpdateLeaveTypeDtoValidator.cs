using FluentValidation;

namespace Hr.LeaveManagement.Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator : AbstractValidator<LeaveTypeDto>
    {
        public UpdateLeaveTypeDtoValidator()
        {
            Include(new ILeaveTypeValidator());

            RuleFor(x => x.Id)
                .NotNull().WithMessage("{PropertyName} must be present.");
        }
    }
}
