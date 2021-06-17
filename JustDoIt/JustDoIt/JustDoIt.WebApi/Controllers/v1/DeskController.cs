using JustDoIt.Application.Attributes;
using JustDoIt.Application.Enums;
using JustDoIt.Application.Features.Desks.Commands.DeleteProductById;
using JustDoIt.Application.Features.Desks.Queries.GetProductById;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Features.Products.Commands.UpdateProduct;
using JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetMyDesks;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.SetCurrentDesk;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JustDoIt.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DeskController : BaseApiController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetMyDesksQuery()));
        }

        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetDeskByIdQuery { Id = id }));
        }

        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpPost]
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

        [Authorize]
        [DeskRole(DeskRoles.User)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateDeskCommand command)
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
            return Ok(await Mediator.Send(new DeleteDeskByIdCommand { Id = id }));
        }
    }
}
