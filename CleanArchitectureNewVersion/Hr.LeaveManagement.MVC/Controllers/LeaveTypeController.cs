using Hr.LeaveManagement.MVC.Contracts;
using Hr.LeaveManagement.MVC.Models;
using Hr.LeaveManagement.MVC.Services;
using Hr.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hr.LeaveManagement.MVC.Controllers
{
    [Authorize]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService leaveTypeService;
        private readonly ILeaveAllocationService leaveAllocationService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService,
            ILeaveAllocationService leaveAllocationService)
        {
            this.leaveTypeService = leaveTypeService;
            this.leaveAllocationService = leaveAllocationService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await this.leaveTypeService.GetLeaveTypes();
            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await this.leaveTypeService.GetLeaveTypeDetails(id);
            return View(model);
        }

        // GET: LeaveTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var response = await this.leaveTypeService.CreateLeaveType(leaveType);
                if (response.Success) 
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await this.leaveTypeService.GetLeaveTypeDetails(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LeaveTypeVM leaveType)
        {
            try
            {
                var response = await this.leaveTypeService.UpdateLeaveType(leaveType);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", response.ValidationErrors);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await this.leaveTypeService.DeleteLeaveType(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Allocate(int id)
        {
            try
            {
                var response = await this.leaveAllocationService.CreateLeaveAllocations(id);
                if (response.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return BadRequest();
        }
    }
}
