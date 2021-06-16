using AutoMapper;
using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.SetCurrentDesk
{
    public partial class SetCurrentDeskCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
    public class InviteCommandHandler : IRequestHandler<SetCurrentDeskCommand, Response<string>>
    {
        private readonly IMemoryCache _cache;
        public InviteCommandHandler(IMemoryCache cache)
        {
            _cache = cache;
        }
        
        public async Task<Response<string>> Handle(SetCurrentDeskCommand command, CancellationToken cancellationToken)
        {
            _cache.Set("currentDesk", command.Id);
            return new Response<string>("Current desk has been selected");
        }
    }
}
