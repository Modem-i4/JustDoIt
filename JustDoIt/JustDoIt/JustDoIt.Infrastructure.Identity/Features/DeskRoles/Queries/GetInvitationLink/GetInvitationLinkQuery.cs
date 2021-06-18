using AutoMapper;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Queries.GetInvitationLink
{
    public class GetInvitationLinkQuery : IRequest<Response<string>>
    {
        public int DeskId { get; set; }
        public class GetInvitationLinkQueryHandler : IRequestHandler<GetInvitationLinkQuery, Response<string>>
        {
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _context;
            public GetInvitationLinkQueryHandler(IMapper mapper, IHttpContextAccessor context)
            {
                _mapper = mapper;
                _context = context;
            }
            public Task<Response<string>> Handle(GetInvitationLinkQuery query, CancellationToken cancellationToken)
            {
                return Task.FromResult(new Response<string>($"{_context.HttpContext.Request.Host}/api/v1/DeskRoles/invite/{query.DeskId}"));
            }
        }
    }
}
