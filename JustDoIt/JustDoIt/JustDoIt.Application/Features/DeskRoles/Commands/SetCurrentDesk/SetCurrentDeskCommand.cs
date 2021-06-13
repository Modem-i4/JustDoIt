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

namespace JustDoIt.Application.Features.DeskRoles.Features.Commands.SetCurrentDesk
{
    public partial class SetCurrentDeskCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
    public class InviteCommandHandler : IRequestHandler<SetCurrentDeskCommand, Response<string>>
    {
        private readonly IMemoryCache _cache;
        private readonly IDeskRepositoryAsync _deskRepository;
        public InviteCommandHandler(IMemoryCache cache, IDeskRepositoryAsync deskRepository)
        {
            _cache = cache;
            _deskRepository = deskRepository;
        }
        
        public async Task<Response<string>> Handle(SetCurrentDeskCommand command, CancellationToken cancellationToken)
        {
            if(!await _deskRepository.Any(command.Id))
            {
                throw new ApiException($"There is no desk with id {command.Id}");
            }
            _cache.Set("currentDesk", command.Id);
            return new Response<string>("Current desk has been selected");
        }
    }
}
