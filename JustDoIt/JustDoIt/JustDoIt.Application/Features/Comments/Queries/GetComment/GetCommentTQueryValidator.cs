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

namespace JustDoIt.Application.Features.Comments.Queries.GetComment
{
    public class GetCommentTQueryValidator : AbstractValidator<GetCommentTQuery>
    {
        private readonly ITaskRepositoryAsync _taskRepository;
        public GetCommentTQueryValidator()
        {
            RuleFor(p => p.TaskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(DoTaskExist).WithMessage("{PropertyName} doesn`t exist.");
        }

        public Task<bool> DoTaskExist(int taskId, CancellationToken cancellationToken)
        {
            return _taskRepository.AnyAsync(taskId);
        }
    }
}
