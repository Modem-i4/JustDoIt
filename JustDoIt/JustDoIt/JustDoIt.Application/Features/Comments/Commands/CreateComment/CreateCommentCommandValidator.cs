using FluentValidation;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        private readonly ICommentRepositoryAsync commentRepository;

        public CreateCommentCommandValidator(ICommentRepositoryAsync commentRepository)
        {
            this.commentRepository = commentRepository;

            /*RuleFor(p => p.OwnerId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniqueOwnerId).WithMessage("{PropertyName} already exists.");
            */
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} must not have at least 3 characters.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
                

        }

    }
}
