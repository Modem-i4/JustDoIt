using AutoMapper;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.DeskRoles.Queries
{
    public class GetParticipantsQuery : IRequest<Response<List<GetParticipantsViewModel>>>
    {
        public int DeskId { get; set; }
        public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, Response<List<GetParticipantsViewModel>>>
        {
            private readonly IDeskRolesService _deskRolesService;
            private readonly IMapper _mapper;
            public GetParticipantsQueryHandler(IDeskRolesService deskRolesService, IMapper mapper)
            {
                _deskRolesService = deskRolesService;
                _mapper = mapper;
            }
            public async Task<Response<List<GetParticipantsViewModel>>> Handle(GetParticipantsQuery query, CancellationToken cancellationToken)
            {
                var participants = await _deskRolesService.GetParticipants(query);
                return new Response<List<GetParticipantsViewModel>>(participants);
            }
        }
    }
}
