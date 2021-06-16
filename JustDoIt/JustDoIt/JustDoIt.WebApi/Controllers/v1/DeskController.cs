using JustDoIt.Application.Features.Products.Commands;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Features.Products.Commands.DeleteProductById;
using JustDoIt.Application.Features.Products.Commands.UpdateProduct;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;
using JustDoIt.Application.Features.Products.Queries.GetProductById;
using JustDoIt.Application.Filters;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.SetCurrentDesk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JustDoIt.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DeskController : BaseApiController
    {
        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllDesksParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllDesksQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetDeskByIdQuery { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post(CreateDeskCommand command)
        {
            var response = await Mediator.Send(command);
            if (response.Succeeded)
            {
                await Mediator.Send(new SetCurrentDeskCommand { Id = response.Data });
                await Mediator.Send(new AddOwnerCommand { DeskId = response.Data });
            }
            return Ok(response);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Put(int id, UpdateDeskCommand command)
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
            return Ok(await Mediator.Send(new DeleteDeskByIdCommand { Id = id }));
        }
    }
}
