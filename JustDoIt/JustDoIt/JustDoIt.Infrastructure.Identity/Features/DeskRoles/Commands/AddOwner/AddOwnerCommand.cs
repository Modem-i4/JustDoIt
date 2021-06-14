using AutoMapper;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Wrappers;
using JustDoIt.Infrastructure.Identity.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner
{
    public partial class AddOwnerCommand : IRequest<Response<string>>
    {
        public int DeskId { get; set; }
    }
    public class AddOwnerCommandHandler : IRequestHandler<AddOwnerCommand, Response<string>>
    {
        private readonly IDeskRolesService _deskRolesService;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IDateTimeService _dateTimeService;
        public AddOwnerCommandHandler(IDeskRolesService deskRolesService, IAuthenticatedUserService authenticatedUser, IDateTimeService dateTimeService)
        {
            _deskRolesService = deskRolesService;
            _authenticatedUser = authenticatedUser;
            _dateTimeService = dateTimeService;
        }

        public async Task<Response<string>> Handle(AddOwnerCommand request, CancellationToken cancellationToken)
        {
            var response = await _deskRolesService.AddUser(new UserDeskRole
            {
                DeskId = request.DeskId,
                UserId = _authenticatedUser.UserId,
                Role = Application.Enums.DeskRoles.Owner,
                Created = _dateTimeService.NowUtc
            });
            return response;
        }
    }
}
