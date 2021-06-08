using JustDoIt.Application.Features.Columns.Queries.GetDeskColumn;
using JustDoIt.Application.Interfaces.Repositories;
using JustDoIt.Domain.Entities;
using JustDoIt.Infrastructure.Persistence.Contexts;
using JustDoIt.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace JustDoIt.Infrastructure.Persistence.Repositories
{
    public class ColumnRepositoryAsync : GenericRepositoryAsync<Column>, IColumnRepositoryAsync
    {
        private readonly DbSet<Column> _columns;

        public ColumnRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _columns = dbContext.Set<Column>();
        }
        public async Task<IEnumerable<Column>> GetColumnsByDeskId(GetDeskColumnsParameter filter)
        {
            return await _columns.Where(o => o.DeskId == filter.DeskId).ToListAsync();
        }
        public Task<bool> ColumnExists(int columnId)
        {
            return _columns.AnyAsync(o => o.Id == columnId);
        }
    }
}
