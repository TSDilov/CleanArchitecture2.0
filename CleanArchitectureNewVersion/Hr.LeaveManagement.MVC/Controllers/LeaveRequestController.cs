﻿using AutoMapper;
using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models.LeaveRequest;
using Hr.LeaveManagement.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hr.LeaveManagement.MVC.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveTypeService leaveTypeService;
        private readonly ILeaveRequestService leaveRequestService;
        private readonly IMapper mapper;

        public LeaveRequestController(ILeaveTypeService leaveTypeService, ILeaveRequestService leaveRequestService,
            IMapper mapper)
        {
            this.leaveTypeService = leaveTypeService;
            this.leaveRequestService = leaveRequestService;
            this.mapper = mapper;
        }

        public async Task<ActionResult> Create()
        {
            var leaveTypes = await this.leaveTypeService.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            var model = new CreateLeaveRequestVM
            {
                LeaveTypes = leaveTypeItems
            };
            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveRequestVM leaveRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await this.leaveRequestService.CreateLeaveRequest(leaveRequest);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", response.ValidationErrors);
            }

            var leaveTypes = await this.leaveTypeService.GetLeaveTypes();
            var leaveTypeItems = new SelectList(leaveTypes, "Id", "Name");
            leaveRequest.LeaveTypes = leaveTypeItems;

            return View(leaveRequest);
        }

        public async Task<ActionResult> Index()
        {
            var model = await this.leaveRequestService.GetAdminLeaveRequestList();
            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await this.leaveRequestService.GetLeaveRequest(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ApproveRequest(int id, bool approved)
        {
            try
            {
                await leaveRequestService.ApproveLeaveRequest(id, approved);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
