using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Tasks.Commands.CheckTask
{
    public class CheckTaskCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public class CheckTaskCommandHandler : IRequestHandler<CheckTaskCommand, Response<string>>
        {
            private readonly ITaskRepositoryAsync _taskRepository;
            public CheckTaskCommandHandler(ITaskRepositoryAsync taskRepository)
            {
                _taskRepository = taskRepository;
            }
            public async Task<Response<string>> Handle(CheckTaskCommand command, CancellationToken cancellationToken)
            {
                var task = await _taskRepository.GetByIdAsync(command.Id);

                do
                {
                    task.Checked = !task.Checked;
                    await _taskRepository.UpdateAsync(task);
                    task = task.ParentTask;
                }
                while (task != null && task.Checked && _taskRepository.IsAllSubtaskChecked(task.ParentTaskId).Result);

                return new Response<string>($"Task has been inverted") { Succeeded = true, Data = Convert.ToString(command.Id) };
                
            }
        }
    }
}
