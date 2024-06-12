using OutOfOffice.Models;

namespace OutOfOffice.Managers
{
    public interface IManager
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(AddEditEmployeeViewModel employeeViewModel);
        Task EditEmployeeAsync(int id, AddEditEmployeeViewModel employeeViewModel);
        Task DeleteEmployeeAsync(int id);
        Task<List<LeaveRequest>> GetLeaveRequestsAsync();
        Task<List<ApprovalRequest>> GetApprovalRequestsAsync();
        Task<List<Project>> GetProjectsAsync();
    }
}