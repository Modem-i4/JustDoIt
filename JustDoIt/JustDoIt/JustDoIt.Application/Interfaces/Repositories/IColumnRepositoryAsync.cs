using JustDoIt.Application.Features.Columns.Queries.GetDeskColumn;
using JustDoIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Application.Interfaces.Repositories
{
    public interface IColumnRepositoryAsync : IGenericRepositoryAsync<Column>
    {
        Task<IEnumerable<Column>> GetColumnsByDeskId(GetDeskColumnsParameter filter);
        Task<bool> ColumnExists(int columnId);
    }
}
