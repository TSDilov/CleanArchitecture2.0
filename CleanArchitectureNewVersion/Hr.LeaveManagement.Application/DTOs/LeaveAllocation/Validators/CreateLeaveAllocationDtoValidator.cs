using FluentValidation;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.DTOs.LeaveType.Validators;

namespace Hr.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await this.leaveTypeRepository.Exists(id);
                    return leaveTypeExists;
                })
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}
