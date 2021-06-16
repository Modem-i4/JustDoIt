using JustDoIt.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetParticipants
{
    public class GetParticipantsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Application.Enums.DeskRoles Role { get; set; }
    }
}
