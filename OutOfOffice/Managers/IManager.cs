using OutOfOffice.Models;

namespace OutOfOffice.Managers
{
    public interface IManager
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(AddEmployeeViewModel employeeViewModel);
        Task EditEmployeeAsync(int id, AddEmployeeViewModel employeeViewModel);
        Task<List<LeaveRequest>> GetLeaveRequestsAsync();
        Task<List<ApprovalRequest>> GetApprovalRequestsAsync();
        Task<List<Project>> GetProjectsAsync();
    }
}