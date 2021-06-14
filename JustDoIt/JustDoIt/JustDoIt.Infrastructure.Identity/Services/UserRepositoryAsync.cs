using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Infrastructure.Identity.Contexts;
using JustDoIt.Infrastructure.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Shared.Services
{
    public class UserRepositoryAsync : IUserRepositoryAsync
    {
        private readonly DbSet<ApplicationUser> _users;
        public ILogger<UserRepositoryAsync> _logger { get; }

        public UserRepositoryAsync(ILogger<UserRepositoryAsync> logger, IdentityContext dbContext)
        {
            _logger = logger;
            _users = dbContext.Set<ApplicationUser>();
        }

        public Task<List<ApplicationUser>> SearchForUsers(string searchQuery)
        {
            var users = _users.Where(q => (q.FirstName + " " + q.LastName + " " + q.NormalizedEmail + " " + q.UserName)
                   .ToLower()
                   .Contains(searchQuery.ToLower()))
                    .ToListAsync();
            return users;
        }

        public Task<bool> AnyAsync(string id)
        {
            return _users.AnyAsync(o => o.Id == id);
        }
    }
}
