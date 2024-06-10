using OutOfOffice.Models;

namespace OutOfOffice.Managers
{
    public interface IManager
    {
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<LeaveRequest>> GetLeaveRequestsAsync();
        Task<List<ApprovalRequest>> GetApprovalRequestsAsync();
        Task<List<Project>> GetProjectsAsync();
    }
}