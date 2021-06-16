using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response<int>>
        {
            private readonly ICommentRepositoryAsync _commentRepository;
            public UpdateCommentCommandHandler(ICommentRepositoryAsync commentRepository)
            {
                _commentRepository = commentRepository;
            }
            public async Task<Response<int>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
            {
                var comment = await _commentRepository.GetByIdAsync(command.Id);

                if (comment == null)
                {
                    throw new ApiException($"Comment Not Found.");
                }
                else
                {
                    comment.Body = command.Body;
                    await _commentRepository.UpdateAsync(comment);
                    return new Response<int>(comment.Id);
                }
            }
        }
    }
}
