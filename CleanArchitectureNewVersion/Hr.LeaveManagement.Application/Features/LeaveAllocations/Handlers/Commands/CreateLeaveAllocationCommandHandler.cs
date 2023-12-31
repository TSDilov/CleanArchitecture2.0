﻿using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Identity;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using Hr.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using Hr.LeaveManagement.Application.Exceptions;
using Hr.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Hr.LeaveManagement.Application.Responses;
using Hr.LeaveManagement.Domain;
using MediatR;

namespace Hr.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IMapper mapper;
        private readonly IUserService userService;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository,
            IMapper mapper,
            IUserService userService,
            ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.mapper = mapper;
            this.userService = userService;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveAllocationDtoValidator(this.leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveAllocationDto);
            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {
                var leaveType = await this.leaveTypeRepository.Get(request.LeaveAllocationDto.LeaveTypeId);
                var employees = await this.userService.GetEmployees();
                var period = DateTime.Now.Year;
                var allocations = new List<Hr.LeaveManagement.Domain.LeaveAllocation>();
                foreach (var employee in employees) 
                {
                    if (await this.leaveAllocationRepository.AllocationExists(employee.Id, leaveType.Id, period)) 
                    {
                        continue;
                    }

                    allocations.Add(new Hr.LeaveManagement.Domain.LeaveAllocation
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveType.Id,
                        NumberDays = leaveType.DefaultDays,
                        Period = period,
                    });
                }
                
                await this.leaveAllocationRepository.AddAllocations(allocations);

                response.Success = true;
                response.Message = "Creation Successful";
            }
            
            return response;
        }
    }
}
