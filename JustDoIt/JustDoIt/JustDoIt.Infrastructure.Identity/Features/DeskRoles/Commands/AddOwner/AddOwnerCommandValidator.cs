using AutoMapper;
using FluentValidation;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Commands.AddOwner
{
    public class AddOwnerCommandValidator : AbstractValidator<AddOwnerCommand>
    {
        private readonly IDeskRolesService _deskRoles;
        private readonly IDeskRepositoryAsync _deskRepository;
        public AddOwnerCommandValidator(IDeskRolesService deskRoles, IDeskRepositoryAsync deskRepository)
        {
            _deskRoles = deskRoles;
            _deskRepository = deskRepository;

            RuleFor(p => p.DeskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(DoDeskEntryExist).WithMessage("This desk doesn't exist.")
                .MustAsync(HasParticipants).WithMessage("You can't become an owner if desk already has participants.");

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
