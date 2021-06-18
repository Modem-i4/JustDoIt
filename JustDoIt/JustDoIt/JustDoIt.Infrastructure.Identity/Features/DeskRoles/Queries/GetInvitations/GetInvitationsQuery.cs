using AutoMapper;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetPendingInvitations
{
    public class GetInvitationsQuery : IRequest<Response<IEnumerable<GetInvitationsViewModel>>>
    {
        public class GetInvitationsQueryHandler : IRequestHandler<GetInvitationsQuery, Response<IEnumerable<GetInvitationsViewModel>>>
        {
            private readonly IDeskRolesService _deskRolesService;
            private readonly IMapper _mapper;
            public GetInvitationsQueryHandler(IDeskRolesService deskRolesService, IMapper mapper)
            {
                _deskRolesService = deskRolesService;
                _mapper = mapper;
            }
            public async Task<Response<IEnumerable<GetInvitationsViewModel>>> Handle(GetInvitationsQuery query, CancellationToken cancellationToken)
            {
                var participantsViewModel = await _deskRolesService.GetInvitationsDesks();
                return new Response<IEnumerable<GetInvitationsViewModel>>(participantsViewModel);
            }
        }
    }
}
