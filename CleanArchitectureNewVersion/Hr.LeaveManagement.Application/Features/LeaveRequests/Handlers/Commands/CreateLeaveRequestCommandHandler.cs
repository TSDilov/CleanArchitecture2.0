using AutoMapper;
using Hr.LeaveManagement.Application.Contracts.Infrastructure;
using Hr.LeaveManagement.Application.Contracts.Persistence;
using Hr.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using Hr.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using Hr.LeaveManagement.Application.Models;
using Hr.LeaveManagement.Application.Responses;
using Hr.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace Hr.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IHttpContextAccessor httpConmtextAccessor;

        public CreateLeaveRequestCommandHandler(
            ILeaveRequestRepository leaveRequestRepository, 
            IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IHttpContextAccessor httpConmtextAccessor)
        {
            this.leaveRequestRepository = leaveRequestRepository;
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.httpConmtextAccessor = httpConmtextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidator(this.leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
            var userId = this.httpConmtextAccessor.HttpContext.User.Claims.FirstOrDefault(
                x => x.Type == "uid")?.Value;
            var allocation = this.leaveAllocationRepository.GetUserAllocations(userId, request.LeaveRequestDto.LeaveTypeId);
            var daysRequired = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;

            if (daysRequired > allocation.Result.NumberDays)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                    nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request"));
            }

            if (!validationResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
            }
            else
            {
                var leaveRequest = this.mapper.Map<LeaveRequest>(request.LeaveRequestDto);
                leaveRequest.RequestingEmployeeId = userId;
                leaveRequest = await this.leaveRequestRepository.Add(leaveRequest);

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveRequest.Id;
            }
                       
            return response;
        }
    }
}
