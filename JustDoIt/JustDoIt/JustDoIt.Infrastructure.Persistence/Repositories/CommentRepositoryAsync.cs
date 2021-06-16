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
using JustDoIt.Application.Features.Comments.Queries.GetComment;

namespace JustDoIt.Infrastructure.Persistence.Repositories
{
    public class CommentRepositoryAsync : GenericRepositoryAsync<Comment>, ICommentRepositoryAsync
    {
        private readonly DbSet<Comment> _columns;

        public CommentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _columns = dbContext.Set<Comment>();
        }
        
        public Task<bool> CommentExists(int columnId)
        {
            return _columns.AnyAsync(o => o.Id == columnId);
        }

        public async Task<IEnumerable<Comment>> GetCommentByTaskskId(GetCommentTParameter filter)
        {
            return await _columns.Where(o => o.TaskId == filter.TaskId).ToListAsync();
        }
    }
}
