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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JustDoIt.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TaskController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetColumnTasksParameter filter)
        {
            return Ok(await Mediator.Send(new GetColumnTasksQuery() {ColumnId = filter.ColumnId }));
        }

        // POST api/<controller>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreateTaskCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Put(int id, UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTaskByIdCommand { Id = id }));
        }
    }
}
