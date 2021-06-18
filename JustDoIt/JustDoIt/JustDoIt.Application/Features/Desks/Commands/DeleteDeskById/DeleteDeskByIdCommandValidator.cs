using FluentValidation;
using JustDoIt.Application.Features.Desks.Commands.DeleteProductById;
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

namespace JustDoIt.Application.Features.Desks.Commands.DeleteDeskById
{
    public class DeleteDeskByIdCommandValidator : AbstractExtendedValidator<DeleteDeskByIdCommand>
    {
        public DeleteDeskByIdCommandValidator(IMemoryCache cache) : base(cache)
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .Must(ValidateDeskId).WithMessage("Open/select this desk first.");
        }
    }
}
