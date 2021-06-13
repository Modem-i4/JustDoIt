using JustDoIt.Application.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustDoIt.Application.Attributes;
using JustDoIt.Application.Enums;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Features.DeskRoles.Features.Commands.Invite;
using JustDoIt.Application.Features.DeskRoles.Commands;
using JustDoIt.Application.Features.DeskRoles.Queries;
using JustDoIt.Application.Features.DeskRoles.Queries.GetParticipants;
using JustDoIt.Application.Features.DeskRoles.Features.Commands.SetCurrentDesk;

namespace JustDoIt.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeskRolesController : BaseApiController
    {
        [Authorize]
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
        [HttpPost("changeRole")]
        [DeskRole(DeskRoles.Manager)]
        public async Task<IActionResult> ChangeRole(ChangeRoleCommand command)
        {
            return Ok(await Mediator.Send(command));
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