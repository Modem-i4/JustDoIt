using JustDoIt.Models;
using JustDoIt.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustDoIt.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private IBaseRepository<TaskModel> Tasks { get; set; }
        private readonly ILogger<TaskController> _logger;

        // The Web API will only accept tokens 1) for users, and 2) having the "access_as_user" scope for this API
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public TaskController(ILogger<TaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TaskModel> Get()
        {
            //HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            var model = Tasks.GetAll();
            return model;
        }
    }
}
