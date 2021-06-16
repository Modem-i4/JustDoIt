using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JustDoIt.Application.Attributes;
using JustDoIt.Application.Enums;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.SetCurrentDesk;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.Invite;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AcceptInvitation;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole;
using JustDoIt.Infrastructure.Identity.Features.Users.Queries.GetInvitationLink;
using JustDoIt.Infrastructure.Identity.Features.Users.Queries.GetParticipants;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetParticipants;
using JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetPendingInvitations;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.RequestInvite;

namespace JustDoIt.WebApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiVersion("1.0")]
    public class DeskRolesController : BaseApiController
    {
        [Authorize]
        [HttpPost("selectDesk")]
        public async Task<IActionResult> SetCurrentDesk(SetCurrentDeskCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [HttpPost("invite")]
        [DeskRole(DeskRoles.Manager)]
        public async Task<IActionResult> InviteAsync(InviteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [HttpGet("getMyInvitations")]
        public async Task<IActionResult> GetPendingInvitations()
        {
            return Ok(await Mediator.Send(new GetInvitationsQuery()));
        }
        [Authorize]
        [HttpPatch("acceptInvite/{id}")]
        public async Task<IActionResult> AcceptInvitationAsync(int id)
        {
            return Ok(await Mediator.Send(new AcceptInvitationCommand { Id = id }));
        }

        [Authorize]
        [HttpGet("getInviteLink")]
        [DeskRole(DeskRoles.Manager)]
        public async Task<IActionResult> GetInvitationLink(int deskId)
        {
            return Ok(await Mediator.Send(new GetInvitationLinkQuery { DeskId = deskId }));
        }
        [Authorize]
        [HttpGet("invite/{id}")]
        public async Task<IActionResult> RequestInviteAsync(int id)
        {
            return Ok(await Mediator.Send(new RequestInviteCommand { DeskId = id }));
        }
        [Authorize]
        [HttpGet("participants")]
        [DeskRole(DeskRoles.Invited)]
        public async Task<IActionResult> GetParticipants([FromQuery] GetParticipantsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [Authorize]
        [HttpPatch("changeRole")]
        [DeskRole(DeskRoles.Manager)]
        public async Task<IActionResult> ChangeRole(ChangeRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("addOwner")]
        public async Task<IActionResult> AddOwner(AddOwnerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}