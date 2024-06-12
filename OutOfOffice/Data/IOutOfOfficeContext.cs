using Microsoft.EntityFrameworkCore;
using OutOfOffice.Models;

namespace OutOfOffice.Data
{
    public interface IOutOfOfficeContext
    {
        DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<LeaveRequest> LeaveRequests { get; set; }
        DbSet<Project> Projects { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}