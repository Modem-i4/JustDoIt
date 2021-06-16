using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JustDoIt.Infrastructure.Identity.Features.Users.Queries;

namespace JustDoIt.WebApi.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [ApiVersion("1.0")]
    public class UserController : BaseApiController
    {
        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}