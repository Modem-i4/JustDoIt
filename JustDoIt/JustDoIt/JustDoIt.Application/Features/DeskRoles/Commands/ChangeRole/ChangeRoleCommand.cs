using AutoMapper;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.DeskRoles.Commands
{
    public partial class ChangeRoleCommand : IRequest<Response<string>>
    {
        public int DeskId { get; set; }
        public string UserId { get; set; }
        public Enums.DeskRoles Role { get; set; }
    }
    public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, Response<string>>
    {
        private readonly IDeskRolesService _deskRolesService;
        private readonly IMapper _mapper;
        public ChangeRoleCommandHandler(IDeskRolesService deskRolesService, IMapper mapper)
        {
            _deskRolesService = deskRolesService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
        {
            var response = await _deskRolesService.ChangeRoleAsync(request);
            return response;
        }
    }
}
