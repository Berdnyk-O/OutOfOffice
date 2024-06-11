﻿using OutOfOffice.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.Models
{
    public class AddEmployeeViewModel
    {
        public string FullName { get; set; } = null!;
        public Subdivision Subdivision { get; set; }
        public Position Position { get; set; }
        public Status Status { get; set; }
        public int? PeoplePartnerId { get; set; }
        public int OutOfOfficeBalance { get; set; }
        public IFormFile Photo { get; set; }

        public EmployeeViewModel[] Partners { get; set; } = null!;

    }
}