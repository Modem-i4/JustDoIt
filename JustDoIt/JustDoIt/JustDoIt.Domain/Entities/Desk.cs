using JustDoIt.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Domain.Entities
{
    public class Desk : AuditableBaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId{ get; set; }
    }
}
