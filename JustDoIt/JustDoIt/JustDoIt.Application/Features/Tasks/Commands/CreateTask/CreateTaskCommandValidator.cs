using FluentValidation;
using JustDoIt.Application.Features.Tasks.Commands.CreateTask;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        private readonly ITaskRepositoryAsync _taskRepository;
        private readonly IColumnRepositoryAsync _columnRepository;

        public CreateTaskCommandValidator(ITaskRepositoryAsync taskRepository, IColumnRepositoryAsync columnRepository)
        {
            _taskRepository = taskRepository;
            _columnRepository = columnRepository;
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} must not have at least 3 characters.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p => p.ColumnId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(ColumnExists).WithMessage("Column does not exist.");
            RuleFor(p => p.ParentTaskId)
               .MustAsync(HasRequiredParent).WithMessage("Parent task does not exist.");
        }
        private async Task<bool> HasRequiredParent(int? parentId, CancellationToken cancellationToken)
        {
            if (parentId == null)
            {
                return true;
            }
            var parentIdInt = parentId ?? 0;
            return await _taskRepository.AnyAsync(parentIdInt);

        }
        private async Task<bool> ColumnExists (int columnId, CancellationToken cancellationToken)
        {
            return await _columnRepository.ColumnExists(columnId);

        }
    }
}
