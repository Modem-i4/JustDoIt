using JustDoIt.Application.Features.Desks.Commands.DeleteProductById;
using JustDoIt.Application.Features.Desks.Queries.GetProductById;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Features.Products.Commands.UpdateProduct;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.SetCurrentDesk;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
