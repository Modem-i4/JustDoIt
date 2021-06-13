using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Domain.Entities;
using JustDoIt.Infrastructure.Persistence.Contexts;
using JustDoIt.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Infrastructure.Persistence.Repositories
{
    public class DeskRepositoryAsync : GenericRepositoryAsync<Desk>, IDeskRepositoryAsync
    {
        private readonly DbSet<Desk> _desks;

        public DeskRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _desks = dbContext.Set<Desk>();
        }

        public Task<bool> Any(int id)
        {
            return _desks.AnyAsync(o => o.Id == id);
        }
    }
}
