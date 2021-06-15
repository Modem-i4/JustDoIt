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

namespace JustDoIt.Application.Features.Products.Commands.CreateProduct
{
    public class CreateDeskCommandValidator : AbstractValidator<CreateDeskCommand>
    {
        public CreateDeskCommandValidator()
        {

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }

    }
}
