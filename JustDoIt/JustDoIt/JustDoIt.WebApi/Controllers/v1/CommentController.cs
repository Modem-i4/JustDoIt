using JustDoIt.Application.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustDoIt.Application.Features.Columns.Queries.GetDeskColumn;
using JustDoIt.Application.Features.Columns.Commands.CreateColumn;
using JustDoIt.Application.Features.Columns.Commands.UpdateColumn;
using JustDoIt.Application.Features.Columns.Commands.DeleteColumnById;
using JustDoIt.Application.Features.Comments.Queries.GetTaskComment;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JustDoIt.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CommentController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetTaskCommentParameter filter)
        {
            return Ok(await Mediator.Send(new GetTaskCommentQuery() { TaskId = filter.TaskId }));
        }

        // POST api/<controller>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreateColumnCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Put(int id, UpdateColumnCommand command)
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
            return Ok(await Mediator.Send(new DeleteColumnByIdCommand { Id = id }));
        }
    }
}
