using FluentBuilder.Core;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.ValueObjects;

namespace ServiceXpert.Application.Services.Contracts;
public interface IIssueService : IServiceBase<int, Issue, IssueDataObject>
{
    Task<(IEnumerable<IssueDataObject>, Pagination)> GetPagedIssuesByStatusAsync(string statusCategory,
        int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null);
}