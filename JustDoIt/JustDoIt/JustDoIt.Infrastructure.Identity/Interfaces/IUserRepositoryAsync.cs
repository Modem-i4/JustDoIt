using JustDoIt.Infrastructure.Identity.Features.Users.Queries;
using JustDoIt.Domain.Entities;
using JustDoIt.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Application.Interfaces.Repositories
{
    public interface IUserRepositoryAsync
    {
        Task<List<ApplicationUser>> SearchForUsers(string searchQuery);
        Task<bool> AnyAsync(string id);
    }
}
