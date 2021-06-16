using AutoMapper;
using FluentValidation;
using JustDoIt.Application;
using JustDoIt.Application.Enums;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using JustDoIt.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.RequestInvite
{
    public class RequestInviteCommandValidator : AbstractValidator<RequestInviteCommand>
    {
        private readonly IDeskRepositoryAsync _deskRepository;
        private readonly IDeskRolesService _deskRolesService;
        private readonly string _userId;
        public RequestInviteCommandValidator(IDeskRepositoryAsync deskRepository, IDeskRolesService deskRolesService, IAuthenticatedUserService authenticatedUser)
        {
            _deskRepository = deskRepository;
            _deskRolesService = deskRolesService;
            _userId = authenticatedUser.UserId;

            RuleFor(p => p.DeskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(DoDeskEntryExist).WithMessage("This desk doesn't exist.")
                .MustAsync(IsEntryUnique).WithMessage("You have already requested this desk permissions.");
        }
        private async Task<bool> DoDeskEntryExist(int id, CancellationToken cancellationToken)
        {
            return await _deskRepository.AnyAsync(id);
        }
        private async Task<bool> IsEntryUnique(int deskId, CancellationToken cancellationToken)
        {
            return !await _deskRolesService.AnyByFilterAsync(deskId, _userId);
        }
    }
}
