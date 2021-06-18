using FluentValidation;
using JustDoIt.Application.Features.Desks.Commands.DeleteProductById;
using JustDoIt.Application.Features.Products.Commands.UpdateProduct;
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

namespace JustDoIt.Application.Features.Desks.Commands.UpdateDesk
{
    public class UpdateDeskCommandValidator : AbstractExtendedValidator<UpdateDeskCommand>
    {
        public UpdateDeskCommandValidator(IMemoryCache cache) : base(cache)
        {
            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .Must(ValidateDeskId).WithMessage("Open/select this desk first.");
        }
    }
}
