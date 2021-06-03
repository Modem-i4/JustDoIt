using AutoMapper;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Columns.Columns.Commands.CreateColumn
{
    public partial class CreateColumnCommand : IRequest<Response<int>>
    {
        public string Title { get; set; }
        public int DeskId { get; set; }
    }
    public class CreateColumnCommandHandler : IRequestHandler<CreateColumnCommand, Response<int>>
    {
        private readonly IColumnRepositoryAsync _columnRepository;
        private readonly IMapper _mapper;
        public CreateColumnCommandHandler(IColumnRepositoryAsync columnRepository, IMapper mapper)
        {
            _columnRepository = columnRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateColumnCommand request, CancellationToken cancellationToken)
        {
            var column = _mapper.Map<Column>(request);
            await _columnRepository.AddAsync(column);
            return new Response<int>(column.Id);
        }
    }
}
