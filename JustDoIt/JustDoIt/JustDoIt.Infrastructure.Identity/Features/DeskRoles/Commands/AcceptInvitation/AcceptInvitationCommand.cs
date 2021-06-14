using AutoMapper;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.AcceptInvitation
{
    public partial class AcceptInvitationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
    public class AcceptInvitationCommandHandler : IRequestHandler<AcceptInvitationCommand, Response<string>>
    {
        private readonly IDeskRolesService _deskRolesService;
        private readonly IMapper _mapper;
        public AcceptInvitationCommandHandler(IDeskRolesService deskRolesService, IMapper mapper)
        {
            _deskRolesService = deskRolesService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
        {
            var response = await _deskRolesService.AcceptInvitation(request);
            return response;
            /*
             * 
            var deskRole = _deskRoles.FirstOrDefault(o => o.Id == command.Id);
            var desk = await _deskRepository.GetByIdAsync(deskRole.DeskId);
            if (deskRole.Role == DeskRoles.Basic)
            {
                throw new ApiException("You are not invited to this desk");
            }
            if(deskRole.Role > DeskRoles.Invited)
            {
                throw new ApiException("You have already accepted this invitation");
            }*/
        }
    }
}
