using AutoMapper;
using JustDoIt.Application.Features.Comments.Queries.GetComment;
using JustDoIt.Application.Filters;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Comments.Queries.GetComment
{
    public class GetCommentTQuery : IRequest<Response<IEnumerable<GetCommentTViewModel>>>
    {
        public int TaskId { get; set; }
    }
    public class GetCommentTQueryHandler : IRequestHandler<GetCommentTQuery, Response<IEnumerable<GetCommentTViewModel>>>
    {
        private readonly ICommentRepositoryAsync _commentRepository;
        private readonly IMapper _mapper;
        public GetCommentTQueryHandler(ICommentRepositoryAsync columnRepository, IMapper mapper)
        {
            _commentRepository = columnRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<GetCommentTViewModel>>> Handle(GetCommentTQuery request, CancellationToken cancellationToken)
        {
            var validFilter = _mapper.Map<GetCommentTParameter>(request);
            var comment = await _commentRepository.GetCommentByTaskskId(validFilter);
            var commentViewModel = _mapper.Map<IEnumerable<GetCommentTViewModel>>(comment);
            return new Response<IEnumerable<GetCommentTViewModel>>(commentViewModel);
        }
    }
}
