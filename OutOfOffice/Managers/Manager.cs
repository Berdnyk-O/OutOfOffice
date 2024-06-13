using Microsoft.EntityFrameworkCore;
using OutOfOffice.Data;
using OutOfOffice.Models;
using OutOfOffice.Models.Entities;

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

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task AddEmployeeAsync(AddEditEmployeeViewModel employeeViewModel)
        {
            string? photoString = null;
            if (employeeViewModel.Photo != null)
            {
                photoString = await PhototoStringAsync(employeeViewModel.Photo);
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

        public async Task EditEmployeeAsync(int id, AddEditEmployeeViewModel employeeViewModel)
        {
            var employee = await GetEmployeeByIdAsync(id);
            if(employee == null)
            {
                return;
            }

            string? photoString = null;
            if (employeeViewModel.Photo != null)
            {
                photoString = await PhototoStringAsync(employeeViewModel.Photo);
            }

            employee.FullName = employeeViewModel.FullName;
            employee.Subdivision = employeeViewModel.Subdivision;
            employee.Position = employeeViewModel.Position;
            employee.Status = employeeViewModel.Status;
            employee.PeoplePartnerId = employeeViewModel.PeoplePartnerId;
            employee.OutOfOfficeBalance = employeeViewModel.OutOfOfficeBalance;
            employee.Photo = photoString;
           
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if(employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<LeaveRequest>> GetLeaveRequestsAsync()
        {
            return await _context.LeaveRequests
                .Include(x => x.Employee)
                .ToListAsync();
        }

        public async Task<LeaveRequest?> GetLeaveRequestByIdAsync(int id)
        {
            return await _context.LeaveRequests
                .Include(x => x.Employee)
                .FirstAsync(x => x.Id == id);
        }

        public async Task<List<ApprovalRequest>> GetApprovalRequestsAsync()
        {
            return await _context.ApprovalRequests
                .Include(x => x.Approver)
                .Include(x=>x.LeaveRequest)
                .ToListAsync();
        }

        public async Task<ApprovalRequest?> GetApprovalRequestByIdAsync(int id)
        {
            return await _context.ApprovalRequests
                .Include(x=>x.Approver)
                .FirstAsync(x=>x.Id == id);
        }

        public async Task<List<Project>> GetProjectsAsync()
        {
            return await _context.Projects
                .Include(x=>x.ProjectManager)
                .ToListAsync();
        }

        private async Task<string> PhototoStringAsync(IFormFile photo)
        {
            string photoString;
            using (var memoryStream = new MemoryStream())
            {
                await photo.CopyToAsync(memoryStream);
                byte[] photoBytes = memoryStream.ToArray();
                photoString = Convert.ToBase64String(photoBytes);
            }
            return photoString;
            
        }

        public async Task UpdateApprovalRequestStatusAsync(int id, ApprovalRequest approvalRequest)
        {
            var request = await GetApprovalRequestByIdAsync(id);
            
            if(request != null)
            {
                request.Comment = approvalRequest.Comment;
                request.Status = approvalRequest.Status;

                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _context.Projects
               .Include(x => x.ProjectManager)
               .FirstAsync(x => x.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(x => x.Employee)
                .FirstAsync(x => x.Email == email);
        }
    }
}
