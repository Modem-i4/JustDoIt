using JustDoIt.Application.Features.Tasks.Queries.GetColumnTasks;
using JustDoIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Application.Interfaces.Repositories
{
    public interface ITaskRepositoryAsync : IGenericRepositoryAsync<TaskModel>
    {
        Task<IEnumerable<TaskModel>> GetTasksByColumnId(GetColumnTasksParameter filter);
    }
}
