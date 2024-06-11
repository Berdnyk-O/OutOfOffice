using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Managers;
using OutOfOffice.Models;

namespace OutOfOffice.Controllers
{
    public class ListsController : Controller
    {
        private readonly IManager _manager;

        public ListsController(IManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult> Employees(string sortBy)
        {
            var employees = await _manager.GetEmployeesAsync();

            switch(sortBy)
            {
                case "FullName":
                    employees = employees.OrderByDescending(x => x.FullName).ToList();
                    break;
                case "Subdivision":
                    employees = employees.OrderByDescending(x => x.Subdivision).ToList();
                    break;
                case "Position":
                    employees = employees.OrderByDescending(x => x.Position).ToList();
                    break;
                case "Status":
                    employees = employees.OrderByDescending(x => x.Status).ToList();
                    break;
                case "Partner":
                    employees = employees.OrderByDescending(x => x.PeoplePartnerId).ToList();
                    break;
                case "OutOfOfficeBalance":
                    employees = employees.OrderByDescending(x => x.OutOfOfficeBalance).ToList();
                    break;
            }

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> AddEmployeeAsync()
        {
            var employees = await _manager.GetEmployeesAsync();
            EmployeeViewModel[] employeesVM = new EmployeeViewModel[employees.Count];
            for (int i = 0; i < employees.Count; i++)
            {
                employeesVM[i] = new EmployeeViewModel()
                {
                    Id = employees[i].Id,
                    FullName = employees[i].FullName,
                    Subdivision = employees[i].Subdivision,
                    Position = employees[i].Position,
                    Status = employees[i].Status,
                    PeoplePartnerId = employees[i].PeoplePartnerId,
                    OutOfOfficeBalance = employees[i].OutOfOfficeBalance,
                    Photo = null
                };
            }

            var addEmployeeVM = new AddEmployeeViewModel()
            {
                Partners = employeesVM
            };

            return View(addEmployeeVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel employeeViewModel)
        {
            await _manager.AddEmployeeAsync(employeeViewModel);
            return RedirectToAction("Employees", "Lists");
        }

        public async Task<ActionResult> LeaveRequests()
        {
            var requests = await _manager.GetLeaveRequestsAsync();
            return View(requests);
        }

        public async Task<ActionResult> ApprovalRequests()
        {
            var requests = await _manager.GetApprovalRequestsAsync();
            return View(requests);
        }

        public async Task<ActionResult> Projects()
        {
            var projects = await _manager.GetProjectsAsync();
            return View(projects);
        }
    }
}
