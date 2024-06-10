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

        public async Task<ActionResult> Employees()
        {
            var employees = await _manager.GetEmployeesAsync();
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
    }
}
