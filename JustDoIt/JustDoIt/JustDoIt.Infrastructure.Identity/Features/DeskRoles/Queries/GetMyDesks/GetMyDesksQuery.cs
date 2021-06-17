using AutoMapper;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Queries.GetMyDesks
{
    public class GetMyDesksQuery : IRequest<Response<IEnumerable<GetMyDesksViewModel>>>
    {
        public int DeskId { get; set; }
        public Application.Enums.ParticipantsFilter ParticipantsFilter { get; set; }
        public class GetMyDesksQueryHandler : IRequestHandler<GetMyDesksQuery, Response<IEnumerable<GetMyDesksViewModel>>>
        {
            private readonly IDeskRolesService _deskRolesService;
            private readonly IMapper _mapper;
            public GetMyDesksQueryHandler(IDeskRolesService deskRolesService, IMapper mapper)
            {
                _deskRolesService = deskRolesService;
                _mapper = mapper;
            }
            public async Task<Response<IEnumerable<GetMyDesksViewModel>>> Handle(GetMyDesksQuery query, CancellationToken cancellationToken)
            {
                var desks = await _deskRolesService.GetMyDesks();
                var desksViewModel = _mapper.Map<IEnumerable<GetMyDesksViewModel>>(desks);
                return new Response<IEnumerable<GetMyDesksViewModel>>(desksViewModel);
            }
        }
    }
}
