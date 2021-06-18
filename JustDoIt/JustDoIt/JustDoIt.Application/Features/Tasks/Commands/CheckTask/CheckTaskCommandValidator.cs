using FluentValidation;
using JustDoIt.Application.Features.Columns.Commands.CreateColumn;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Tasks.Commands.CheckTask
{
    public class CheckTaskCommandValidator : AbstractExtendedValidator<CheckTaskCommand>
    {
        private readonly ITaskRepositoryAsync _taskRepository;
        public CheckTaskCommandValidator(ITaskRepositoryAsync taskRepository, IMemoryCache cache) : base(cache)
        {
            _taskRepository = taskRepository;
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(DoesntHaveSubtasks).WithMessage("{PropertyName} can`t be cheked, because it has children.")
                .MustAsync(DoTaskExist).WithMessage("{PropertyName} doesn`t exist.");
        }

        public Task<bool> DoTaskExist(int taskId, CancellationToken cancellationToken)
        {
            return _taskRepository.AnyAsync(taskId);
        }
        public bool DoesntHaveSubtasks(int taskId)
        {
            return !_taskRepository.HasSubtasks(taskId).Result;
        }
    }
}
