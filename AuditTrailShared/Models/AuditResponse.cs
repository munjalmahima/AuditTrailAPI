using AuditTrailShared.Enums;

namespace AuditTrailShared.Models
{
    public class AuditResponse
    {
        public AuditAction Action { get; set; }
        public List<ChangeLog> Changes { get; set; } = [];
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
    }
}
