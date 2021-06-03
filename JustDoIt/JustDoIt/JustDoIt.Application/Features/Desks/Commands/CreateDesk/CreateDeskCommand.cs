using AutoMapper;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Products.Commands.CreateProduct
{
    public partial class CreateDeskCommand : IRequest<Response<int>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string OwnerId{ get; set; }
    }
    public class CreateDeskCommandHandler : IRequestHandler<CreateDeskCommand, Response<int>>
    {
        private readonly IDeskRepositoryAsync _deskRepository;
        private readonly IMapper _mapper;
        public CreateDeskCommandHandler(IDeskRepositoryAsync deskRepository, IMapper mapper)
        {
            _deskRepository = deskRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateDeskCommand request, CancellationToken cancellationToken)
        {
            var desk = _mapper.Map<Desk>(request);
            await _deskRepository.AddAsync(desk);
            return new Response<int>(desk.Id);
        }
    }
}
