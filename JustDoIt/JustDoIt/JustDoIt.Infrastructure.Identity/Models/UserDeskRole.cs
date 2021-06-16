using JustDoIt.Application.DTOs.Account;
using JustDoIt.Application.Enums;
using JustDoIt.Domain.Common;
using JustDoIt.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Infrastructure.Identity.Models
{
    public class UserDeskRole : AuditableBaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Desk Desk { get; set; }
        public int DeskId { get; set; }
        public DeskRoles Role { get; set; }
    }
}
