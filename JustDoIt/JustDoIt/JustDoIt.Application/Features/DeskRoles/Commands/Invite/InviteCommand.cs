using AutoMapper;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.DeskRoles.Features.Commands.Invite
{
    public partial class InviteCommand : IRequest<Response<string>>
    {
        public int DeskId { get; set; }
        public string UserId { get; set; }
    }
    public class InviteCommandHandler : IRequestHandler<InviteCommand, Response<string>>
    {
        private readonly IDeskRolesService _deskRolesService;
        public InviteCommandHandler(IDeskRolesService deskRolesService)
        {
            _deskRolesService = deskRolesService;
        }
        
        public async Task<Response<string>> Handle(InviteCommand command, CancellationToken cancellationToken)
        {
            var response = await _deskRolesService.InviteAsync(command);
            return response;
        }
    }
}
