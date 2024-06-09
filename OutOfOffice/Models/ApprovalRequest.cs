using OutOfOffice.Enums;

namespace OutOfOffice.Models
{
    public class ApprovalRequest
    {
        public int Id { get; set; }
        public int ApproverId { get; set; }
        public int LeaveRequestId { get; set; }
        public string? Comment { get; set; }
        public Status Status { get; set; }
    }
}
