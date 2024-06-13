﻿using Microsoft.EntityFrameworkCore;
using OutOfOffice.Models.Entities;

namespace OutOfOffice.Data
{
    public interface IOutOfOfficeContext
    {
        DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<LeaveRequest> LeaveRequests { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}