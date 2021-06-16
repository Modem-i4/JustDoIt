using JustDoIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Domain.Entities
{
    public class Column : AuditableBaseEntity
    {
        public int DeskId { get; set; }
        public Desk Desk { get; set; }
        public string Title { get; set; }
        public IEnumerable<TaskModel> Tasks { get; set; }
    }
}
