using AutoMapper;
using FluentValidation;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Application.Features.DeskRoles.Commands
{
    public class CreateDeskCommandValidator : AbstractValidator<ChangeRoleCommand>
    {

        public CreateDeskCommandValidator()
        {
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }
    }
}
