using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories;
using ServiceXpert.Infrastructure.DbContexts;

namespace ServiceXpert.Infrastructure.Repositories;
public class CommentRepository : RepositoryBase<Guid, Comment>, ICommentRepository
{
    public CommentRepository(SxpDbContext dbContext) : base(dbContext)
    {
    }
}
