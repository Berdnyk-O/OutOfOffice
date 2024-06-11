using Microsoft.AspNetCore.Mvc;
using OutOfOffice.Data;
using OutOfOffice.Managers;

namespace OutOfOffice.Controllers
{
    public class ListsController : Controller
    {
        private readonly IManager _manager;

        public ListsController(IManager manager)
        {
            _manager = manager;
        }

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
