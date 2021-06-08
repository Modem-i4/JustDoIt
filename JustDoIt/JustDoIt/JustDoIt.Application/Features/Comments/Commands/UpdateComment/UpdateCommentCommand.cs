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
        public string Title { get; set; }
        public string Description { get; set; }
        public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response<int>>
        {
            private readonly IDeskRepositoryAsync _deskRepository;
            public UpdateCommentCommandHandler(IDeskRepositoryAsync deskRepository)
            {
                _deskRepository = deskRepository;
            }
            public async Task<Response<int>> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
            {
                var desk = await _deskRepository.GetByIdAsync(command.Id);

                if (desk == null)
                {
                    throw new ApiException($"Desk Not Found.");
                }
                else
                {
                    desk.Title = command.Title;
                    desk.Description = command.Description;
                    await _deskRepository.UpdateAsync(desk);
                    return new Response<int>(desk.Id);
                }
            }
        }
    }
}
