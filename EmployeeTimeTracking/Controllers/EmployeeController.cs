using EmployeeTimeTracking.Commands;
using EmployeeTimeTracking.Models;
using EmployeeTimeTracking.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTimeTracking.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Employee Controller
        /// </summary>
        /// <param name="mediator"></param>
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns>List of all emlpoyees</returns>
        // GET: /Employee
        public async Task<IActionResult> Index()
        {
            var query = new GetAllEmployeesQuery();
            var employees = await _mediator.Send(query);
            return View(employees);
        }

        /// <summary>
        /// Details 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Employee details by Epmployee Id</returns>
        // GET: /Employee/Details/{id}
        public async Task<IActionResult> Details(int id)
        {
            var query = new GetEmployeeQuery { Id = id };
            var employee = await _mediator.Send(query);
            return employee != null ? View(employee) : NotFound();
        }

        /// <summary>
        /// Create a new Employee view
        /// </summary>
        /// <returns></returns>
        // GET: /Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a new Employee object
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: /Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var command = new AddEmployeeCommand { Employee = model };
                var result = await _mediator.Send(command);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Unable to create employee.");
            }
            return View(model);
        }

        /// <summary>
        /// Edit Employee page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: /Employee/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var query = new GetEmployeeQuery { Id = id };
            var employee = await _mediator.Send(query);
            return employee != null ? View(employee) : NotFound();
        }

        /// <summary>
        /// Update Employee data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: /Employee/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeRequestModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var command = new UpdateEmployeeCommand { Employee = model };
                var result = await _mediator.Send(command);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Unable to update employee.");
            }
            return View(model);
        }

        /// <summary>
        /// recalculate total hours for all employees
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> RecalculateTotalHours()
        {
            var command = new RecalculateTotalHoursCommand();
            var result = await _mediator.Send(command);

            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Unable to recalculate total hours.");
            return RedirectToAction(nameof(Index));
        }

    }
}
