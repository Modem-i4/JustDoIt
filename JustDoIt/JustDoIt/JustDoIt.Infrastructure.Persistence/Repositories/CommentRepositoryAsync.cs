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
    public class CommentRepositoryAsync : GenericRepositoryAsync<Comment>, ICommentRepositoryAsync
    {
        private readonly DbSet<Comment> _comments;

        public CommentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _comments = dbContext.Set<Comment>();
        }
  
    }
}
