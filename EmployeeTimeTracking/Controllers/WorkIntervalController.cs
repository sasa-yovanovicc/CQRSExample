using AutoMapper;
using EmployeeTimeTracking.Commands;
using EmployeeTimeTracking.Interfaces;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Queries;
using EmployeeTimeTracking.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeTimeTracking.Controllers
{
    public class WorkIntervalController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWorkIntervalService _workIntervalService;

        public WorkIntervalController(IMediator mediator, IMapper mapper, IWorkIntervalService workIntervalService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _workIntervalService = workIntervalService;
        }

        /// <summary>
        /// List of all working intervals for certain employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Start(int? id, int employeeId)
        {
            var query = new GetEmployeeByIdQuery { Id = employeeId };
            var employee = await _mediator.Send(query);

            if (employee == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<WorkIntervalRequestModel>(employee);
            if (id.HasValue)
            {
                model.Id = id.Value;
            }
            return View(model);
        }

        /// <summary>
        /// Save Start work time page (prichod)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveStart(WorkIntervalSaveRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new AddWorkIntervalCommand { WorkInterval = model };
                var result = await _mediator.Send(command);
                if (result)
                {
                    return RedirectToAction("Index", "Employee");
                }
                ModelState.AddModelError("", "Unable to start work interval.");
            }
            return View(model);
        }

        /// <summary>
        /// Update Start work time (prichod)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStart(WorkIntervalUpdateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateWorkIntervalCommand { WorkInterval = model };
                var result = await _mediator.Send(command);
                if (result)
                {
                    return RedirectToAction("ListWorkIntervals", new { employeeId = model.EmployeeId });
                }
                ModelState.AddModelError("", "Unable to update work interval start.");
            }
            return View(model); // Adjust if necessary to return to a specific view
        }

        /// <summary>
        /// End work time page (odchod)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IActionResult> End(int? id, int employeeId)
        {
            var query = new GetEmployeeByIdQuery { Id = employeeId };
            var employee = await _mediator.Send(query);

            if (employee == null)
            {
                return NotFound();
            }

            var model = _mapper.Map<WorkIntervalRequestModel>(employee);
            if (id.HasValue)
            {
                model.Id = id.Value;
            }
            return View(model);
        }

        /// <summary>
        /// Save End work time (odchod)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEnd(WorkIntervalSaveRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new AddWorkIntervalCommand { WorkInterval = model };
                var result = await _mediator.Send(command);
                if (result)
                {
                    return RedirectToAction("Index", "Employee");
                }
                ModelState.AddModelError("", "Unable to end work interval.");
            }
            return View(model);
        }

        /// <summary>
        /// Update ENd work time (odchod)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEnd(WorkIntervalUpdateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new UpdateWorkIntervalCommand { WorkInterval = model };
                var result = await _mediator.Send(command);
                if (result)
                {
                    return RedirectToAction("ListWorkIntervals", new { employeeId = model.EmployeeId });
                }
                ModelState.AddModelError("", "Unable to update work interval end.");
            }
            return View(model); // Adjust if necessary to return to a specific view
        }

        /// <summary>
        /// List all work intervals for seleted employee page 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IActionResult> ListWorkIntervals(int employeeId)
        {
            // Get intervals from the mediator
            var query = new GetWorkIntervalsByEmployeeIdQuery { EmployeeId = employeeId };
            var intervals = await _mediator.Send(query);

            // Get merged intervals and total hours from the service
            var (sortedIntervals, totalHours, mergedIntervals) = _workIntervalService.GetWorkIntervalsWithTotalHours(intervals);

            return View((sortedIntervals, totalHours, employeeId));
        }
    }
}
