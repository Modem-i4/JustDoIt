using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Comments.Commands.DeleteCommentById
{
    public class DeleteCommentByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteCommentByIdCommandHandler : IRequestHandler<DeleteCommentByIdCommand, Response<int>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;
            public DeleteCommentByIdCommandHandler(ICommentRepositoryAsync commentRepository)
            {
                _commentRepository = commentRepository;
            }
            public async Task<Response<int>> Handle(DeleteCommentByIdCommand command, CancellationToken cancellationToken)
            {
                var comment = await _commentRepository.GetByIdAsync(command.Id);
                if (comment == null) throw new ApiException($"Comment Not Found.");
                await _commentRepository.DeleteAsync(comment);
                return new Response<int>(comment.Id);
            }
        }
    }
}
