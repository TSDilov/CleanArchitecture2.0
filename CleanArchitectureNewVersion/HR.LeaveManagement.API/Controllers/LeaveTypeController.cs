﻿using Hr.LeaveManagement.Application.DTOs.LeaveType;
using Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using Hr.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using Hr.LeaveManagement.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
            var leaveTypes = await this.mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveTypeDto>> GetById(int id)
        {
            var leaveType = await this.mediator.Send(new GetLeaveTypeDetailsRequest { Id = id });
            return Ok(leaveType);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeDto leaveType)
        {
            var command = new CreateLeaveTypeCommand { LeaveTypeDto = leaveType };
            var response = await this.mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] LeaveTypeDto leaveType)
        {
            var command = new UpdateLeavetypeCommand { LeaveTypeDto = leaveType };
            await this.mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand { Id = id };
            await this.mediator.Send(command);
            return NoContent();
        }        
    }
}
