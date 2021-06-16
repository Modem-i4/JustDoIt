using AutoMapper;
using JustDoIt.Application.Enums;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using JustDoIt.Infrastructure.Identity.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.RequestInvite
{
    public partial class RequestInviteCommand : IRequest<Response<string>>
    {
        public int DeskId { get; set; }
    }
    public class RequestInviteCommandHandler : IRequestHandler<RequestInviteCommand, Response<string>>
    {
        private readonly IDeskRolesService _deskRolesService;
        private readonly IAuthenticatedUserService _authenticatedUser;
        private readonly IMapper _mapper;
        public RequestInviteCommandHandler(IDeskRolesService deskRolesService, IMapper mapper, IAuthenticatedUserService authenticatedUser)
        {
            _deskRolesService = deskRolesService;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Response<string>> Handle(RequestInviteCommand command, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<UserDeskRole>(command);
            model.UserId = _authenticatedUser.UserId;
            model.Role = Application.Enums.DeskRoles.Pending;
            var response = await _deskRolesService.AddUser(model);
            return response;
        }
    }
}
