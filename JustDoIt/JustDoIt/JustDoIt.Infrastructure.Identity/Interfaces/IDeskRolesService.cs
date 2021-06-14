using JustDoIt.Application.Enums;
using JustDoIt.Application.Wrappers;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AcceptInvitation;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.Invite;
using JustDoIt.Infrastructure.Identity.Features.Users.Queries.GetParticipants;
using JustDoIt.Infrastructure.Identity.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JustDoIt.Application.Interfaces
{
    public interface IDeskRolesService
    {
        public int CurrentDeskId { get; }
        public DeskRoles CurrentDeskRole { get; }
        Task<Response<string>> AddUser(UserDeskRole userDeskRole);
        Task<Response<string>> ChangeRoleAsync(ChangeRoleCommand command);
        Task<List<ApplicationUser>> GetParticipants(GetParticipantsQuery query);
        Task<Response<string>> AcceptInvitation(AcceptInvitationCommand command);
        Task<bool> AnyAsync(int id);
        Task<bool> AnyByFilterAsync(int deskId, string userId);
        Task<bool> HasParticipants(int deskId);
    }
}
