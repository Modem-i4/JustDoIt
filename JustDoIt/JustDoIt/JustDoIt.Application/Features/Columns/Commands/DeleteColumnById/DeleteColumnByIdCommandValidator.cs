using FluentValidation;
using JustDoIt.Application.Features.Columns.Commands.CreateColumn;
using JustDoIt.Application.Features.Columns.Commands.DeleteColumnById;
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

namespace JustDoIt.Application.Features.Columns.Commands.DeleteColumnById
{
    public class DeleteColumnByIdCommandValidator : AbstractExtendedValidator<DeleteColumnByIdCommand>
    {
        public DeleteColumnByIdCommandValidator(IMemoryCache cache) : base(cache)
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}
