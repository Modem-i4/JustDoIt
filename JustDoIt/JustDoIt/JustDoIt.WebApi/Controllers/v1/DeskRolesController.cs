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

namespace JustDoIt.WebApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiVersion("1.0")]
    public class DeskRolesController : BaseApiController
    {
        //[Authorize]
        [HttpPost("setCurrentDesk")]
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
        [HttpPatch("invite/{id}")]
        [DeskRole(DeskRoles.Manager)]
        public async Task<IActionResult> AcceptInvitationAsync(int id)
        {
            return Ok(await Mediator.Send(new AcceptInvitationCommand { Id = id }));
        }
        [Authorize]
        [HttpPatch("changeRole")]
        [DeskRole(DeskRoles.Manager)]
        public async Task<IActionResult> ChangeRole(ChangeRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [HttpPost("addOwner")]
        public async Task<IActionResult> AddOwner(AddOwnerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        //[Authorize]
        [HttpGet("getLink")]
        //[DeskRole(DeskRoles.Manager)]
        public async Task<IActionResult> GetInvitationLink(int deskId)
        {
            return Ok(await Mediator.Send(new GetInvitationLinkQuery { DeskId = deskId }));
        }
        [Authorize]
        [HttpGet("participants/{deskId}")]
        [DeskRole(DeskRoles.Invited)]
        public async Task<IActionResult> GetParticipants(int deskId)
        {
            return Ok(await Mediator.Send(new GetParticipantsQuery { DeskId = deskId }));
        }
    }
}