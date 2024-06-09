using OutOfOffice.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OutOfOffice.Models
{
    public class ApprovalRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Employee")]
        public int ApproverId { get; set; }
        public Employee Approver { get; set; } = null!;
        public int LeaveRequestId { get; set; }
        public string? Comment { get; set; }
        public Status Status { get; set; }
    }
}
