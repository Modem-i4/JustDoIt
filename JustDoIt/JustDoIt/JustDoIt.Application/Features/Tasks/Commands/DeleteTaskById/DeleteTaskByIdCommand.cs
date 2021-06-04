using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Tasks.Commands.DeleteTaskById
{
    public class DeleteTaskByIdCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public class DeleteTaskByIdCommandHandler : IRequestHandler<DeleteTaskByIdCommand, Response<int>>
        {
            private readonly ITaskRepositoryAsync _taskRepository;
            public DeleteTaskByIdCommandHandler(ITaskRepositoryAsync taskRepository)
            {
                _taskRepository = taskRepository;
            }
            public async Task<Response<int>> Handle(DeleteTaskByIdCommand command, CancellationToken cancellationToken)
            {
                var task = await _taskRepository.GetByIdAsync(command.Id);
                if (task == null) throw new ApiException($"Task Not Found.");
                await _taskRepository.DeleteAsync(task);
                return new Response<int>(task.Id);
            }
        }
    }
}
