using System;
using System.Collections.Generic;
using System.Text;
using JustDoIt.Domain.Common;

namespace JustDoIt.Domain.Entities
{
    public class Comment : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int TaskId { get; set; }
        public TaskModel Task { get; set; }
    }
}
