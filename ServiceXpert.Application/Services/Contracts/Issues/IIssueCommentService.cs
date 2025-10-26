using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Models;
using ServiceXpert.Domain.Entities.Issues;

namespace ServiceXpert.Application.Services.Contracts.Issues;
public interface IIssueCommentService : IServiceBase<Guid, IssueComment, IssueCommentDataObject>
{
    Task<ServiceResult<IEnumerable<IssueCommentDataObject>>> GetAllByIssueKeyAsync(string issueKey, CancellationToken cancellationToken = default);
}
