using JustDoIt.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Application.DTOs.Account
{
    public class ChangeRoleRequest
    {
        public string UserId { get; set; }
        public DeskRoles Role { get; set; }
    }
}
