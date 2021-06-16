using AutoMapper;
using JustDoIt.Application.Interfaces;
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
        public string Body { get; set; }
        public int TaskId { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Response<int>>
    {
        private readonly ICommentRepositoryAsync _commentRepository;
        private readonly IMapper _mapper;
        private readonly string _userId;
        public CreateCommentCommandHandler(ICommentRepositoryAsync columnRepository, IMapper mapper, IAuthenticatedUserService authenticatedUserService)
        {
            _commentRepository = columnRepository;
            _mapper = mapper;
            _userId = authenticatedUserService.UserId;
        }

        public async Task<Response<int>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Comment>(request);
            comment.UserId = _userId;
            await _commentRepository.AddAsync(comment);
            return new Response<int>(comment.Id);
        }
    }
}
