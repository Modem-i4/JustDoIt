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
    public class UpdateTaskCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Checked { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ColumnId { get; set; }
        public int ParentTaskId { get; set; }

        public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Response<int>>
        {
            private readonly ITaskRepositoryAsync _taskRepository;
            public UpdateTaskCommandHandler(ITaskRepositoryAsync taskRepository)
            {
                _taskRepository = taskRepository;
            }
            public async Task<Response<int>> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
            {
                var task = await _taskRepository.GetByIdAsync(command.Id);

                if (task == null)
                {
                    throw new ApiException($"Task Not Found.");
                }
                else
                {
                    task.Title = command.Title;
                    task.Checked = command.Checked;
                    task.StartDate = command.StartDate;
                    task.EndDate = command.EndDate;
                    await _taskRepository.UpdateAsync(task);
                    return new Response<int>(task.Id);
                }
            }
        }
    }
}
