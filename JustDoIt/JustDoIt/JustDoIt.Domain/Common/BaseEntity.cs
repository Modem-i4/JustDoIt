﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
    }
}
