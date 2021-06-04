using AutoMapper;
using JustDoIt.Application.Filters;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Columns.Queries.GetDeskColumn
{
    public class GetDeskColumnsQuery : IRequest<Response<IEnumerable<GetDeskColumnsViewModel>>>
    {
        public int DeskId { get; set; }
    }
    public class GetDeskColumnsQueryHandler : IRequestHandler<GetDeskColumnsQuery, Response<IEnumerable<GetDeskColumnsViewModel>>>
    {
        private readonly IColumnRepositoryAsync _columnRepository;
        private readonly IMapper _mapper;
        public GetDeskColumnsQueryHandler(IColumnRepositoryAsync columnRepository, IMapper mapper)
        {
            _columnRepository = columnRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetDeskColumnsViewModel>>> Handle(GetDeskColumnsQuery request, CancellationToken cancellationToken)
        {
            //TODO: implify filter
            var validFilter = _mapper.Map<GetDeskColumnsParameter>(request);
            var column = await _columnRepository.GetColumnsByDeskId(validFilter);
            var columnViewModel = _mapper.Map<IEnumerable<GetDeskColumnsViewModel>>(column);
            return new Response<IEnumerable<GetDeskColumnsViewModel>>(columnViewModel);
        }
    }
}
