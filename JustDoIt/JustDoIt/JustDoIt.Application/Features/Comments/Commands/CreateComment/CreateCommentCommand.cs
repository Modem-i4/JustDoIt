using AutoMapper;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Comments.Commands.CreateComment
{
    public partial class CreateCommentCommand : IRequest<Response<int>>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int TaskId { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<int>>
    {
        private readonly ICommentRepositoryAsync _commentRepository;
        private readonly IMapper _mapper;
        public CreateCommentCommandHandler(ICommentRepositoryAsync commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request);
            await _commentRepository.AddAsync(comment);
            return new Response<int>(comment.Id);
        }
    }
}
