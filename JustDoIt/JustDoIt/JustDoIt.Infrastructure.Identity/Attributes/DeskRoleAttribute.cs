using JustDoIt.Application.Enums;
using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace JustDoIt.Application.Attributes
{
    public class DeskRoleAttribute : ActionFilterAttribute
    {
        private readonly DeskRoles _requiredRole;
        public DeskRoleAttribute(DeskRoles requiredRole)
        {
            _requiredRole = requiredRole;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _deskRolesService = (IDeskRolesService)context.HttpContext.RequestServices.GetService(typeof(IDeskRolesService));
            if (_deskRolesService.CurrentDeskRole < _requiredRole)
            {
                context.Result = new UnauthorizedObjectResult($"Role \"{_deskRolesService.CurrentDeskRole}\" is not authorized to do this. \"{_requiredRole}\" role is required.\nMake sure you have selected the required desk");
            }
            else 
                base.OnActionExecuting(context);
        }
    }
}
