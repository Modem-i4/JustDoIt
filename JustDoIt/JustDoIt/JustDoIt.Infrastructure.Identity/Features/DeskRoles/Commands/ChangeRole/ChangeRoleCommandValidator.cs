using AutoMapper;
using FluentValidation;
using JustDoIt.Application.Features.Products.Commands.CreateProduct;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole
{
    public class ChangeRoleCommandValidator : AbstractValidator<ChangeRoleCommand>
    {
        private readonly IDeskRolesService _deskRoles;
        public ChangeRoleCommandValidator(IDeskRolesService deskRoles)
        {
            _deskRoles = deskRoles;

            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            RuleFor(m => m)
                .MustAsync(DoEntryExist).WithMessage("This user is not invited to the desk.");
        }

        private async Task<bool> DoEntryExist(ChangeRoleCommand changeRole, CancellationToken cancellationToken)
        {
            return await _deskRoles.AnyByFilterAsync(changeRole.DeskId, changeRole.UserId);
        }
    }
}
