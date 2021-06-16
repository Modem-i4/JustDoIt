using AutoMapper;
using FluentValidation;
using JustDoIt.Application;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Commands.AddOwner
{
    public class AddOwnerCommandValidator : AbstractExtendedValidator<AddOwnerCommand>
    {
        private readonly IDeskRolesService _deskRoles;
        private readonly IDeskRepositoryAsync _deskRepository;
        public AddOwnerCommandValidator(IDeskRolesService deskRoles, IDeskRepositoryAsync deskRepository, IMemoryCache cache) : base(cache)
        {
            _deskRoles = deskRoles;
            _deskRepository = deskRepository;

            RuleFor(p => p.DeskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(DoDeskEntryExist).WithMessage("This desk doesn't exist.")
                .MustAsync(HasParticipants).WithMessage("You can't become an owner if desk already has participants.")
                .Must(ValidateDeskId).WithMessage("Open/select this desk first.");

        }

        private async Task<bool> HasParticipants(int deskId, CancellationToken cancellationToken)
        {
            return !await _deskRoles.HasParticipants(deskId);
        }
        private async Task<bool> DoDeskEntryExist(int id, CancellationToken cancellationToken)
        {
            return await _deskRepository.AnyAsync(id);
        }
    }
}
