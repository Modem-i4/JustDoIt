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
        private readonly ITaskRepositoryAsync taskRepository;
        private readonly IColumnRepositoryAsync columnRepository;

        public CreateTaskCommandValidator(ITaskRepositoryAsync taskRepository, IColumnRepositoryAsync columnRepository)
        {
            this.taskRepository = taskRepository;
            this.columnRepository = columnRepository;
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} must not have at least 3 characters.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p => p.ColumnId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(ColumnExists).WithMessage("Column does not exist.");
        }
        private async Task<bool> ColumnExists (int columnId, CancellationToken cancellationToken)
        {
            return await columnRepository.ColumnExists(columnId);

        }
    }
}
