using JustDoIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Domain.Entities
{
    public class Comment : AuditableBaseEntity
    {
        public string Body { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public TaskModel Task { get; set; }
    }
}
