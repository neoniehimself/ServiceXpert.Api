using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Models.Issues.QueryOptions;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.ValueObjects.Pagination;

namespace ServiceXpert.Application.Services.Contracts.Issues;
public interface IIssueService : IServiceBase<int, Issue, IssueDataObject>
{
    Task<ServiceResult<PaginationResult<IssueDataObject>>> GetPagedIssuesAsync(GetPagedIssuesQueryOption? queryOption = null, IncludeOptions<Issue>? includeOptions = null, CancellationToken cancellationToken = default);
}
