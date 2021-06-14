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

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.Invite
{
    public partial class InviteCommand : IRequest<Response<string>>
    {
        public int DeskId { get; set; }
        public string UserId { get; set; }
    }
    public class InviteCommandHandler : IRequestHandler<InviteCommand, Response<string>>
    {
        private readonly IDeskRolesService _deskRolesService;
        private readonly IMapper _mapper;
        public InviteCommandHandler(IDeskRolesService deskRolesService, IMapper mapper)
        {
            _deskRolesService = deskRolesService;
            _mapper = mapper;
        }
        
        public async Task<Response<string>> Handle(InviteCommand command, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<UserDeskRole>(command);
            model.Role = Application.Enums.DeskRoles.Invited;
            var response = await _deskRolesService.AddUser(model);
            return response;
        }
    }
}
