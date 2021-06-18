using FluentValidation;
using JustDoIt.Application.Features.Desks.Commands.DeleteProductById;
using JustDoIt.Application.Features.Desks.Queries.GetProductById;
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

namespace JustDoIt.Application.Features.Desks.Queries.GetDeskById
{
    public class GetDeskByIdQuerydValidator : AbstractExtendedValidator<GetDeskByIdQuery>
    {
        private readonly IDeskRepositoryAsync _deskRepository;
        public GetDeskByIdQuerydValidator(IDeskRepositoryAsync deskRepository, IMemoryCache cache) : base(cache)
        {
            _deskRepository = deskRepository;
            RuleFor(p => p.Id)
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
