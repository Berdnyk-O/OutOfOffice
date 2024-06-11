using Microsoft.EntityFrameworkCore;
using OutOfOffice.Data;
using OutOfOffice.Enums;
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

        public async Task AddEmployeeAsync(AddEmployeeViewModel employeeViewModel)
        {
            string photoString;
            using (var memoryStream = new MemoryStream())
            {
                await employeeViewModel.Photo.CopyToAsync(memoryStream);
                byte[] photoBytes = memoryStream.ToArray();
                photoString = Convert.ToBase64String(photoBytes);
            }

            var employee = new Employee()
            {
                FullName = employeeViewModel.FullName,
                Subdivision = employeeViewModel.Subdivision,
                Position = employeeViewModel.Position,
                Status = employeeViewModel.Status,
                PeoplePartnerId = employeeViewModel.PeoplePartnerId,
                OutOfOfficeBalance = employeeViewModel.OutOfOfficeBalance,
                Photo = photoString
            };

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
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

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _context.Projects
                .Include(x=>x.ProjectManager)
                .ToListAsync();
        }
    }
}
