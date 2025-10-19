using ServiceXpert.Application.DataObjects.Comment;
using ServiceXpert.Application.Shared;
using ServiceXpert.Domain.Entities.Issues;

namespace ServiceXpert.Application.Services.Contracts;
public interface ICommentService : IServiceBase<Guid, IssueComment, CommentDataObject>
{
    Task<Result<IEnumerable<CommentDataObject>>> GetAllByIssueKeyAsync(int issueId);
}
