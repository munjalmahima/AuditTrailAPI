using AuditTrailShared.Enums;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace AuditTrailShared.Models
{
    public class AuditRequest
    {
        public JObject? Before { get; set; }
        public JObject? After { get; set; }

        [Required]
        public AuditAction Action { get; set; }

        [Required, MinLength(1)]
        public string UserId { get; set; } = string.Empty;

        [Required, MinLength(1)]
        public string EntityName { get; set; } = string.Empty;
    }
}
