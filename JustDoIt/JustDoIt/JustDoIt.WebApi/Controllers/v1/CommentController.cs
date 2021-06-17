using JustDoIt.Application.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustDoIt.Application.Features.Comments.Queries.GetComment;
using JustDoIt.Application.Features.Comments.Commands.CreateComment;
using JustDoIt.Application.Features.Comments.Commands.UpdateComment;
using JustDoIt.Application.Features.Comments.Commands.DeleteCommentById;
using JustDoIt.Application.Attributes;
using JustDoIt.Application.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JustDoIt.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CommentController : BaseApiController
    {
        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetCommentTParameter filter)
        {
            return Ok(await Mediator.Send(new GetCommentTQuery() { TaskId = filter.TaskId }));
        }

        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateCommentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCommentCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCommentByIdCommand { Id = id }));
        }
    }
}
