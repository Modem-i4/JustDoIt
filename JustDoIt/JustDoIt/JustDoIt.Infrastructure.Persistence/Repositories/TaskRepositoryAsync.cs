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
using JustDoIt.Application.Interfaces;

namespace JustDoIt.Infrastructure.Persistence.Repositories
{
    public class TaskRepositoryAsync : GenericRepositoryAsync<TaskModel>, ITaskRepositoryAsync
    {
        private readonly DbSet<TaskModel> _tasks;
        private readonly string _userId;

        public TaskRepositoryAsync(ApplicationDbContext dbContext, IAuthenticatedUserService authenticatedUser) : base(dbContext)
        {
            _userId = authenticatedUser.UserId;
            _tasks = dbContext.Set<TaskModel>();
        }

        public Task<bool> AnyAsync(int taskId)
        {
            return _tasks.AnyAsync(o => o.Id == taskId);
        }

        public async Task<IEnumerable<TaskModel>> GetTasksByFilter(GetColumnTasksParameter filter)
        {
            var baseQuery = _tasks.Include(o=>o.Column.Desk).Where(o => o.Column.DeskId == filter.DeskId);
            switch (filter.TaskMode)
            {
                case Application.Enums.TaskListModes.ClosestDeadlines:
                    return await _tasks.Where(o => !o.Checked)
                        .OrderBy(o => o.EndDate)
                        .Take(filter.TAmount)
                        .ToListAsync();
                case Application.Enums.TaskListModes.AssignedToMe:
                    return await _tasks.Where(o => o.AssignedToUserId == _userId && !o.Checked)
                        .Take(filter.TAmount)
                        .ToListAsync();
                default:
                    return await baseQuery.ToListAsync();
            }
        }

        public Task<bool> HasSubtasks(int taskId)
        {
            return _tasks.AnyAsync(o => o.ParentTaskId == taskId);
        }

        public Task<bool> IsAllSubtaskChecked(int? parentId)
        {
            return _tasks.Where(o => o.ParentTaskId == parentId).AllAsync(o => o.Checked);
        }
    }
}
