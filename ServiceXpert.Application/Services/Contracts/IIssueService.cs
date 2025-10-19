using FluentBuilder.Core;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Application.Shared;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.ValueObjects;

namespace ServiceXpert.Application.Services.Contracts;
public interface IIssueService : IServiceBase<int, Issue, IssueDataObject>
{
    Task<Result<PagedResult<IssueDataObject>>> GetPagedIssuesByStatusAsync(string statusCategory, int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null);
}
