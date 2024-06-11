using OutOfOffice.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public Subdivision Subdivision { get; set; }
        public Position Position { get; set; }
        public Status Status { get; set; }
        public int? PeoplePartnerId { get; set; }
        public int OutOfOfficeBalance { get; set; }
        public string? Photo { get; set; }
    }
}
