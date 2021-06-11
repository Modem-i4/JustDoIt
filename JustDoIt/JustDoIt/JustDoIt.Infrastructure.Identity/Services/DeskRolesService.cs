using JustDoIt.Application.DTOs.Account;
using JustDoIt.Application.DTOs.Email;
using JustDoIt.Application.Enums;
using JustDoIt.Application.Exceptions;
using JustDoIt.Application.Features.DeskRoles.Commands;
using JustDoIt.Application.Features.DeskRoles.Features.Commands.Invite;
using JustDoIt.Application.Features.DeskRoles.Queries;
using JustDoIt.Application.Features.Products.Queries.GetAllProducts;
using JustDoIt.Application.Interfaces;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Application.Wrappers;
using JustDoIt.Domain.Settings;
using JustDoIt.Infrastructure.Identity.Contexts;
using JustDoIt.Infrastructure.Identity.Models;
using JustDoIt.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
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
            IAuthenticatedUserService authenticatedUserService, IMemoryCache cache, IDateTimeService dateTimeService)
        {
            _logger = logger;
            _deskRepository = deskRepository;
            _userId = authenticatedUserService.UserId;
            _deskRoles = dbContext.Set<UserDeskRole>();
            _dbContext = dbContext;
            _dateTimeService = dateTimeService;

            CurrentDeskId = cache.Get<int>("currentDesk");
        }

        public async Task<Response<string>> InviteAsync(InviteCommand command)
        {
            var desk = await _deskRepository.GetByIdAsync(command.DeskId);
            if (desk == null)
            {
                throw new ApiException($"This desk doesn't exist.");
            }
            var model = await _deskRoles.FirstOrDefaultAsync(o => o.DeskId == command.DeskId && o.UserId == command.UserId);
            if(model != null)
            {
                throw new ApiException($"This user is already invited to the desk.");
            }
            var entity = new UserDeskRole
            {
                UserId = command.UserId,
                DeskId = command.DeskId,
                Role = DeskRoles.Invited,
                Created = _dateTimeService.NowUtc
            };
            try
            {
                var result = _dbContext.Add(entity);
                await _dbContext.SaveChangesAsync();
                model = _deskRoles.Include(o => o.User).First(o => o.Id == result.Entity.Id);
                return new Response<string>(Convert.ToString(result.Entity.Id), message: $"{model.User.FirstName} {model.User.LastName} has been invited to participate \"{desk.Title}\" desk.");
            }
            catch (Exception)
            {
                throw new ApiException($"An error occured while saving changes to DB.");
            }
        }

        public async Task<Response<string>> ChangeRoleAsync(ChangeRoleCommand command)
        {
            var desk = await _deskRepository.GetByIdAsync(command.DeskId);
            if (desk == null)
            {
                throw new ApiException($"This desk doesn't exist.");
            }
            var model = await _deskRoles.Include(o => o.User)
                .FirstOrDefaultAsync(o => o.DeskId == command.DeskId && o.UserId == command.UserId);
            if (model == null)
            {
                throw new ApiException($"This user is not invited to the desk.");
            }
            model.Role = command.Role;
            var result = _deskRoles.Update(model);

            try
            {
                await _dbContext.SaveChangesAsync();
                return new Response<string>(Convert.ToString(result.Entity.Id), message: $"{model.User.FirstName} {model.User.LastName} is in role of {model.Role} for \"{desk.Title}\" desk now.");
            }
            catch (Exception)
            {
                throw new ApiException($"An error occured while saving changes to DB.");
            }
        }

        public Task<List<GetParticipantsViewModel>> GetParticipants(GetParticipantsQuery query)
        {
            var users = _deskRoles.Include(o => o.User).Where(o => o.DeskId == query.DeskId).Select(o => new GetParticipantsViewModel { 
            Name = $"{o.User.FirstName} {o.User.LastName}",
            Role = o.Role
            }).ToListAsync();
            return users;
        }

        private Task<DeskRoles> GetAccessLevel(int deskId)
        {
            var role = _deskRoles.FirstOrDefaultAsync(o => o.UserId == _userId && o.DeskId == deskId).ContinueWith(
                o => o.Result == null ? DeskRoles.Basic : o.Result.Role);
            return role;
        }

    }
}
