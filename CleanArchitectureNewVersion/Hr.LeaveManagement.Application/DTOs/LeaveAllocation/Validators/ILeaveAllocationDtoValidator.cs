using FluentValidation;
using Hr.LeaveManagement.Application.Contracts.Persistence;

namespace Hr.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public ILeaveAllocationDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            RuleFor(x => x.NumberDays)
                .GreaterThan(0).WithMessage("{PropertyName} must be more than {ComparisonValue}!");

            RuleFor(x => x.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisonValue}!");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExist = await this.leaveTypeRepository.Exists(id);
                    return !leaveTypeExist;
                }).WithMessage("{PropertyName} does not exist!");
        }
    }
}
