using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
    }
}
