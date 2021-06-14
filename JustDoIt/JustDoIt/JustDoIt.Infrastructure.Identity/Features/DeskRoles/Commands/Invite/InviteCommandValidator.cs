using AutoMapper;
using FluentValidation;
using JustDoIt.Application.Enums;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Entities;
using JustDoIt.Infrastructure.Identity.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Identity.Features.Users.Commands.Invite
{
    public class InviteCommandValidator : AbstractValidator<InviteCommand>
    {
        private readonly IDeskRepositoryAsync _deskRepository;
        private readonly IUserRepositoryAsync _userRepository;
        public InviteCommandValidator(IDeskRepositoryAsync deskRepository, IUserRepositoryAsync userRepository)
        {
            _deskRepository = deskRepository;
            _userRepository = userRepository;

            RuleFor(p => p.DeskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(DoDeskEntryExist).WithMessage("This desk doesn't exist.");
            RuleFor(p => p.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(DoUserEntryExist).WithMessage("This user doesn't exist.");


        }
        private async Task<bool> DoDeskEntryExist(int id, CancellationToken cancellationToken)
        {
            return await _deskRepository.AnyAsync(id);
        }
        private async Task<bool> DoUserEntryExist(string id, CancellationToken cancellationToken)
        {
            return await _userRepository.AnyAsync(id);
        }
    }
}
