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

            var addEmployeeVM = new AddEditEmployeeViewModel()
            {
                Partners = employeesVM
            };

            return View(addEmployeeVM);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(AddEditEmployeeViewModel employeeViewModel)
        {
            await _manager.AddEmployeeAsync(employeeViewModel);
            return RedirectToAction("Employees", "Lists");
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employees = await _manager.GetEmployeesAsync();

            var employee = employees.Find(x=>x.Id==id);
            if(employee == null)
            {
                return View();
            }

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

            var addEmployeeVM = new AddEditEmployeeViewModel()
            {
                FullName = employee.FullName,
                Subdivision = employee.Subdivision,
                Position = employee.Position,
                Status = employee.Status,
                PeoplePartnerId = employee.PeoplePartnerId,
                OutOfOfficeBalance = employee.OutOfOfficeBalance,
                PhotoBase64 = employee.Photo,
                Partners = employeesVM
            };

            return View(addEmployeeVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(int id, AddEditEmployeeViewModel employeeViewModel)
        {
            await _manager.EditEmployeeAsync(id, employeeViewModel);
            return RedirectToAction("Employees", "Lists");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _manager.DeleteEmployeeAsync(id);
            return RedirectToAction("Employees", "Lists");
        }

        [HttpGet]
        public async Task<ActionResult> LeaveRequests()
        {
            var requests = await _manager.GetLeaveRequestsAsync();
            return View(requests);
        }

        [HttpGet]
        public async Task<ActionResult> ApprovalRequests(string sortBy)
        {
            var requests = await _manager.GetApprovalRequestsAsync();

            switch (sortBy)
            {
                case "ApproverFullName":
                    requests = requests.OrderByDescending(x => x.Approver.FullName).ToList();
                    break;
                case "LeaveRequestId":
                    requests = requests.OrderByDescending(x => x.LeaveRequestId).ToList();
                    break;
                case "Status":
                    requests = requests.OrderByDescending(x => x.Status).ToList();
                    break;
            }

            return View(requests);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsApprovalRequest(int id)
        {
            var request = await _manager.GetApprovalRequestByIdAsync(id);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DetailsApprovalRequest(int id, ApprovalRequest approvalRequest)
        {
            await _manager.UpdateApprovalRequestStatusAsync(id, approvalRequest);
            return RedirectToAction("ApprovalRequests", "Lists");
        }

        [HttpGet]
        public async Task<ActionResult> Projects()
        {
            var projects = await _manager.GetProjectsAsync();
            return View(projects);
        }
    }
}
