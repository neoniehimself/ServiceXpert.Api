using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Application.Shared;
using ServiceXpert.Domain.Entities;

namespace ServiceXpert.Application.Services.Contracts;
public interface ICommentService : IServiceBase<Guid, Comment, CommentDataObject>
{
    Task<Result<IEnumerable<CommentDataObject>>> GetAllByIssueKeyAsync(int issueId);
}
