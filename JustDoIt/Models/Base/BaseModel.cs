using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustDoIt.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
    }
}
