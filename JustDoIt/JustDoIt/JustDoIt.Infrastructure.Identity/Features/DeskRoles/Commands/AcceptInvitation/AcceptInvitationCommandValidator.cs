using AutoMapper;
using FluentValidation;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AcceptInvitation;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.DeskRoles.Commands.AcceptInvitation
{
    public class AcceptInvitationCommandValidator : AbstractValidator<AcceptInvitationCommand>
    {
        private readonly IDeskRolesService _deskRoles;
        private readonly IDeskRepositoryAsync _deskRepository;
        public AcceptInvitationCommandValidator(IDeskRolesService deskRoles, IDeskRepositoryAsync deskRepository)
        {
            _deskRoles = deskRoles;
            _deskRepository = deskRepository;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MustAsync(DoInavitationExist).WithMessage("You were not invited to this desk.");

        }

        private async Task<bool> DoInavitationExist(int id, CancellationToken cancellationToken)
        {
            return await _deskRoles.AnyAsync(id);
        }
    }
}
