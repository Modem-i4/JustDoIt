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

namespace JustDoIt.Application.Features.Tasks.Queries.GetColumnTasks
{
    public class GetColumnTasksQueryValidator : AbstractExtendedValidator<GetColumnTasksQuery>
    {
        private readonly IDeskRepositoryAsync _deskRepository;

        public GetColumnTasksQueryValidator(IDeskRepositoryAsync deskRepository, IMemoryCache cache) : base(cache)
        {
            _deskRepository = deskRepository;

            RuleFor(p => p.DeskId)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .Must(ValidateDeskId).WithMessage("Open/select this desk first.")
               .MustAsync(DoDeskExist).WithMessage("{PropertyName} doesn`t exist.");
        }

        public Task<bool> DoDeskExist(int deskId, CancellationToken cancellationToken)
        {
            return _deskRepository.AnyAsync(deskId);
        }
    }
}
