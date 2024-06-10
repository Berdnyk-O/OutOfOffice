using Microsoft.EntityFrameworkCore;
using OutOfOffice.Data;
using OutOfOffice.Models;

namespace OutOfOffice.Managers
{
    public class Manager : IManager
    {
        private readonly IOutOfOfficeContext _context;

        public Manager(IOutOfOfficeContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                .Include(x => x.Employee)
                .ToListAsync();
        }

        public async Task<List<ApprovalRequest>> GetApprovalRequestsAsync()
        {
            return await _context.ApprovalRequests
                .Include(x => x.Approver)
                .Include(x=>x.LeaveRequest)
                .ToListAsync();
        }
    }
}
