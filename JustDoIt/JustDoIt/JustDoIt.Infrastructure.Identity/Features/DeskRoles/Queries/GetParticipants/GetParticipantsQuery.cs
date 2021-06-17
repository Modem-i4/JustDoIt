using AutoMapper;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetParticipants
{
    public class GetParticipantsQuery : IRequest<Response<IEnumerable<GetParticipantsViewModel>>>
    {
        public int DeskId { get; set; }
        public Application.Enums.ParticipantsFilter ParticipantsFilter { get; set; }
        public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, Response<IEnumerable<GetParticipantsViewModel>>>
        {
            private readonly IDeskRolesService _deskRolesService;
            private readonly IMapper _mapper;
            public GetParticipantsQueryHandler(IDeskRolesService deskRolesService, IMapper mapper)
            {
                _deskRolesService = deskRolesService;
                _mapper = mapper;
            }
            public async Task<Response<IEnumerable<GetParticipantsViewModel>>> Handle(GetParticipantsQuery query, CancellationToken cancellationToken)
            {
                var participants = await _deskRolesService.GetParticipants(query);
                var participantsViewModel = _mapper.Map<IEnumerable<GetParticipantsViewModel>>(participants);
                return new Response<IEnumerable<GetParticipantsViewModel>>(participantsViewModel);
            }
        }
    }
}
