using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Tasks.Commands.UpdateTask
{
    public class CheckTaskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public bool Checked { get; set; }

        public class CheckTaskCommandHandler : IRequestHandler<CheckTaskCommand, Response<int>>
        {
            private readonly ITaskRepositoryAsync _taskRepository;
            public CheckTaskCommandHandler(ITaskRepositoryAsync taskRepository)
            {
                _taskRepository = taskRepository;
            }
            public async Task<Response<int>> Handle(CheckTaskCommand command, CancellationToken cancellationToken)
            {
                var task = await _taskRepository.GetByIdAsync(command.Id);

                if (task == null)
                {
                    throw new ApiException($"Task Not Found.");
                }
                if (_taskRepository.HasSubtasks(task.Id).Result)
                {
                    throw new ApiException($"Task cannot be checked.");
                }
                
                task.Checked =!task.Checked;
                if (_taskRepository.IsAllSubtaskChecked(task.ParentTaskId).Result)
                {
                    //todo checkedParentTask
                }
                await _taskRepository.UpdateAsync(task);
                return new Response<int>(task.Id);
                
            }
        }
    }
}
