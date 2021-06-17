using JustDoIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Domain.Entities
{
    public class TaskModel : AuditableBaseEntity
    {
        public string Title { get; set; }
        public bool Checked { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ColumnId { get; set; }
        public Column Column { get; set; }
        public int? ParentTaskId { get; set; }
        public TaskModel ParentTask { get; set; }
        public string AssignedToUserId { get; set; }
    }
}
