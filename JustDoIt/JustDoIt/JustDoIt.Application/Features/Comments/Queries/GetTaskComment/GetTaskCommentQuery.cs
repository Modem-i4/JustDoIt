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

namespace JustDoIt.Application.Features.Products.Queries.GetAllProducts
{
    public class GetTaskCommentQuery : IRequest<Response<IEnumerable<GetAllDesksViewModel>>>
    {
        public int TaskId { get; set; }
    }
    public class GetTaskCommentQueryHandler : IRequestHandler<GetAllDesksQuery, PagedResponse<IEnumerable<GetAllDesksViewModel>>>
    {
        private readonly IDeskRepositoryAsync _deskRepository;
        private readonly IMapper _mapper;
        public GetTaskCommentQueryHandler(IDeskRepositoryAsync deskRepository, IMapper mapper)
        {
            _deskRepository = deskRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<IEnumerable<GetAllDesksViewModel>>> Handle(GetAllDesksQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetAllDesksParameter>(request);
            var desk = await _deskRepository.GetPagedReponseAsync(validFilter.PageNumber, validFilter.PageSize);
            var deskViewModel = _mapper.Map<IEnumerable<GetAllDesksViewModel>>(desk);
            return new PagedResponse<IEnumerable<GetAllDesksViewModel>>(deskViewModel, validFilter.PageNumber, validFilter.PageSize);
        }
    }
}
