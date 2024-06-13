﻿using OutOfOffice.Enums;
using OutOfOffice.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Models
{
    public class AddEditProjectViewModel
    {
        public int Id { get; set; }
        public ProjectType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int ProjectManagerId { get; set; }
        public string? Comment { get; set; }
        public Status Status { get; set; }

        public EmployeeViewModel[] ProjectManagers { get; set; } = null!;
    }
}
