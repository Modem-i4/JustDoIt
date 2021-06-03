using FluentValidation;
using JustDoIt.Application.Columns.Columns.Commands.CreateColumn;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Domain.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Columns.Products.Commands.CreateColumn
{
    public class CreateColumnCommandValidator : AbstractValidator<CreateColumnCommand>
    {
        private readonly IColumnRepositoryAsync columnRepository;

        public CreateColumnCommandValidator(IColumnRepositoryAsync columnRepository)
        {
            this.columnRepository = columnRepository;

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} must not have at least 3 characters.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }
    }
}
