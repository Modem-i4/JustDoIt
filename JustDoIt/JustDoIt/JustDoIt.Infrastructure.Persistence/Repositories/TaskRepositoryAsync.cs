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
using JustDoIt.Application.Features.Tasks.Queries.GetColumnTasks;

namespace JustDoIt.Infrastructure.Persistence.Repositories
{
    public class TaskRepositoryAsync : GenericRepositoryAsync<TaskModel>, ITaskRepositoryAsync
    {
        private readonly DbSet<TaskModel> _tasks;

        public TaskRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _tasks = dbContext.Set<TaskModel>();
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByColumnId(GetColumnTasksParameter filter)
        {
            return await _tasks.Where(o => o.ColumnId == filter.ColumnId).ToListAsync();
        }
    }
}
