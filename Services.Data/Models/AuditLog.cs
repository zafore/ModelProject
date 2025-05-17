using System;
using System.Collections.Generic;

namespace Services.Data.Models
{
    public partial class AuditLog
    {
        public int AuditLogId { get; set; }
        public string TableName { get; set; } = null!;
        public string? PrimaryKeyValue { get; set; }
        public string? Action { get; set; }
        public string? ControllerName { get; set; }
        public string? UserName { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
