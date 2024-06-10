﻿using Microsoft.EntityFrameworkCore;
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
    }
}