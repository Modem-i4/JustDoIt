using JustDoIt.Application.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Queries.GetParticipants
{
    public class GetParticipantsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Application.Enums.DeskRoles Role { get; set; }
    }
}
