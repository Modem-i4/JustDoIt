using JustDoIt.Application.Enums;
using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Infrastructure.Identity.Contexts;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AcceptInvitation;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.AddOwner;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.ChangeRole;
using JustDoIt.Infrastructure.Identity.Features.Users.Commands.Invite;
using JustDoIt.Infrastructure.Identity.Features.Users.Queries.GetParticipants;
using JustDoIt.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Shared.Services
{
    public class DeskRolesService : IDeskRolesService
    {
        private readonly DbSet<UserDeskRole> _deskRoles;
        private readonly string _userId;
        private readonly IdentityContext _dbContext;
        private int _currentDeskId;
        private IDeskRepositoryAsync _deskRepository;

        public ILogger<DeskRolesService> _logger { get; }
        public IDateTimeService _dateTimeService { get; }
        public int CurrentDeskId { 
            get => _currentDeskId; 
            private set {
                if(value != _currentDeskId)
                {
                    var role = GetAccessLevel(deskId: value).Result;
                    if (role > DeskRoles.Invited)
                    {
                        _currentDeskId = value;
                        CurrentDeskRole = role;
                    }
                    else
                    {
                        throw new ApiException($"You are now authorized to see this desk");
                    }
                }
            }
        }
        public DeskRoles CurrentDeskRole { get; private set; }

        public DeskRolesService(ILogger<DeskRolesService> logger, IdentityContext dbContext, IDeskRepositoryAsync deskRepository,
            IAuthenticatedUserService authenticatedUserService, IMemoryCache cache)
        {
            _logger = logger;
            _deskRepository = deskRepository;
            _userId = authenticatedUserService.UserId;
            _deskRoles = dbContext.Set<UserDeskRole>();
            _dbContext = dbContext;

            CurrentDeskId = cache.Get<int>("currentDesk");
        }

        public async Task<Response<string>> AddUser(UserDeskRole userDeskRole)
        {
            try
            {
                var result = _dbContext.Add(userDeskRole);
                await _dbContext.SaveChangesAsync();
                var model = _deskRoles.Include(o => o.User).First(o => o.Id == result.Entity.Id);
                var desk = await _deskRepository.GetByIdAsync(userDeskRole.DeskId);
                return new Response<string>(Convert.ToString(result.Entity.Id), 
                    message: $"{model.User.FirstName} {model.User.LastName} has joined \"{desk.Title}\" desk with \"{userDeskRole.Role}\" role.");
            }
            catch (Exception)
            {
                throw new ApiException($"An error occured while saving changes to DB.");
            }
        }

        public async Task<Response<string>> ChangeRoleAsync(ChangeRoleCommand command)
        {
            var model = await _deskRoles.Include(o => o.User)
                .FirstOrDefaultAsync(o => o.DeskId == command.DeskId && o.UserId == command.UserId);
            model.Role = command.Role;
            try
            {
                var result = _deskRoles.Update(model);
                await _dbContext.SaveChangesAsync();
                var desk = await _deskRepository.GetByIdAsync(command.DeskId);
                return new Response<string>(Convert.ToString(result.Entity.Id), message: $"{model.User.FirstName} {model.User.LastName} is in role of {model.Role} for \"{desk.Title}\" desk now.");
            }
            catch (Exception)
            {
                throw new ApiException($"An error occured while saving changes to DB.");
            }
        }

        public Task<List<ApplicationUser>> GetParticipants(GetParticipantsQuery query)
        {
            var users = _deskRoles.Include(o => o.User).Where(o => o.DeskId == query.DeskId).Select(o => o.User).ToListAsync();
            return users;
        }

        private Task<DeskRoles> GetAccessLevel(int deskId)
        {
            var role = _deskRoles.FirstOrDefaultAsync(o => o.UserId == _userId && o.DeskId == deskId).ContinueWith(
                o => o.Result == null ? DeskRoles.Basic : o.Result.Role);
            return role;
        }

        public async Task<Response<string>> AcceptInvitation(AcceptInvitationCommand command)
        {
            var deskRole = _deskRoles.FirstOrDefault(o => o.Id == command.Id);
            deskRole.Role = DeskRoles.User;
            try
            {
                await _dbContext.SaveChangesAsync();
                var desk = await _deskRepository.GetByIdAsync(deskRole.DeskId);
                return new Response<string>(Convert.ToString(command.Id), $"You have received User role for \"{desk.Title}\" desk!");
            }
            catch (Exception)
            {
                throw new ApiException("An error occured while saving changes to DB.");
            }
        }

        public Task<bool> AnyAsync(int id)
        {
            return _deskRoles.AnyAsync(o => o.Id == id);
        }

        public Task<bool> AnyByFilterAsync(int deskId, string userId = null)
        {
            userId ??= _userId;
            return _deskRoles.AnyAsync(o => o.UserId == userId && o.DeskId == deskId);
        }

        public Task<bool> HasParticipants(int deskId)
        {
            return _deskRoles.AnyAsync(o => o.DeskId == deskId);
        }
    }
}
