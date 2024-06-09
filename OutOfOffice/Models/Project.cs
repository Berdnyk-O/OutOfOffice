using OutOfOffice.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace OutOfOffice.Models
{
    public class Project
    {
        public int Id { get; set; }
        public ProjectType Type { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        [ForeignKey("Employee")]
        public int ProjectManagerId { get; set; }
        public Employee ProjectManager { get; set; } = null!;
        public string? Comment { get; set; }
        public Status Status { get; set; }
    }
}
