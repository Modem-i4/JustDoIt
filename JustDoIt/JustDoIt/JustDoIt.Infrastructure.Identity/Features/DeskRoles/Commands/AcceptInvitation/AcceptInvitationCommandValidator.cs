using AutoMapper;
using FluentValidation;
using JustDoIt.Application;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AcceptInvitation;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole;
using JustDoIt.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
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
        private Application.Enums.DeskRoles _role;
        public AcceptInvitationCommandValidator(IDeskRolesService deskRoles)
        {
            _deskRoles = deskRoles;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(IsAuthorizedToThisDesk).WithMessage("You were not invited to this desk.")
                .Must(IsNotAlreadyAMemver).WithMessage("You have already accepted this invitation.");
        }

        private bool IsAuthorizedToThisDesk(int id)
        {
            var invitation = _deskRoles.GetInvitation(id).Result;
            if (invitation == null)
                return false;
            _role = invitation.Role;
            return _role > Application.Enums.DeskRoles.Pending;
        }
        private bool IsNotAlreadyAMemver(int id)
        {
            return _role < Application.Enums.DeskRoles.User;
        }
    }
}
