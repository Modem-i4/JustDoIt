using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Products.Queries.GetProductById
{
    public class GetDeskByIdQuery : IRequest<Response<Desk>>
    {
        public int Id { get; set; }
        public class GetDeskByIdQueryHandler : IRequestHandler<GetDeskByIdQuery, Response<Desk>>
        {
            private readonly IDeskRepositoryAsync _deskRepository;
            private readonly IMemoryCache _cache;
            public GetDeskByIdQueryHandler(IDeskRepositoryAsync deskRepository, IMemoryCache cache)
            {
                _deskRepository = deskRepository;
                _cache = cache;
            }
            public async Task<Response<Desk>> Handle(GetDeskByIdQuery query, CancellationToken cancellationToken)
            {
                var desk = await _deskRepository.GetByIdAsync(query.Id);
                if (desk == null) throw new ApiException($"Desk Not Found.");
                _cache.Set("currentDesk", query.Id);
                return new Response<Desk>(desk);
            }
        }
    }
}
