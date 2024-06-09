using OutOfOffice.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int EmployeId{ get; set; }
        public Employee Employee { get; set; } = null!;
        public AbsenceReason AbsenceReason { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string? Comment { get; set; }
        public Status Status { get; set; }

    }
}
