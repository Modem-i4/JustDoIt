using AutoMapper;
using FluentValidation;
using JustDoIt.Application;
using JustDoIt.Application.Enums;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.SetCurrentDesk;
using JustDoIt.Infrastructure.Identity.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.SetCurrentDesk
{
    public class SetCurrentDeskCommandValidator : AbstractValidator<SetCurrentDeskCommand>
    {
        private readonly IDeskRepositoryAsync _deskRepository;
        public SetCurrentDeskCommandValidator(IDeskRepositoryAsync deskRepository)
        {
            _deskRepository = deskRepository;

            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(DoDeskEntryExist).WithMessage("This desk doesn't exist.");
        }
        private async Task<bool> DoDeskEntryExist(int id, CancellationToken cancellationToken)
        {
            return await _deskRepository.AnyAsync(id);
        }
    }
}
