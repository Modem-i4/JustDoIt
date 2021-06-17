using JustDoIt.Application.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustDoIt.Application.Features.Tasks.Queries.GetColumnTasks;
using JustDoIt.Application.Features.Tasks.Commands.CreateTask;
using JustDoIt.Application.Features.Tasks.Commands.UpdateTask;
using JustDoIt.Application.Features.Tasks.Commands.DeleteTaskById;
using JustDoIt.Application.Features.Tasks.Commands.CheckTask;
using JustDoIt.Application.Attributes;
using JustDoIt.Application.Enums;

namespace JustDoIt.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TaskController : BaseApiController
    {
        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetColumnTasksParameter filter)
        {
            return Ok(await Mediator.Send(new GetColumnTasksQuery() {DeskId = filter.DeskId, TaskMode = filter.TaskMode, TAmount = 5 }));
        }

        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpPost]
        public async Task<IActionResult> Post(CreateTaskCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpPatch]
        public async Task<IActionResult> Check(CheckTaskCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateTaskCommand command)
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
            return Ok(await Mediator.Send(new DeleteTaskByIdCommand { Id = id }));
        }
    }
}
