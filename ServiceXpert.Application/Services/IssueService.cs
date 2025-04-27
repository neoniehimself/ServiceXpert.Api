using FluentBuilder.Core;
using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Issue;
using ServiceXpert.Application.Services.Contracts;
using ServiceXpert.Domain.Entities;
using ServiceXpert.Domain.Repositories.Contracts;
using ServiceXpert.Domain.ValueObjects;
using DomainEnums = ServiceXpert.Domain.Shared.Enums;

namespace ServiceXpert.Application.Services;
public class IssueService : ServiceBase<int, Issue, IssueDataObject>, IIssueService
{
    private readonly IIssueRepository issueRepository;

    public IssueService(IIssueRepository issueRepository, IMapper mapper) : base(issueRepository, mapper)
    {
        this.issueRepository = issueRepository;
    }

    private static int GetIdFromKey(string issueKey)
    {
        try
        {
            if (int.TryParse(issueKey.Split('-')[1], out int issueId))
            {
                return issueId;
            }
        }
        catch (IndexOutOfRangeException e)
        {
            throw new IndexOutOfRangeException("Failed to extract Id from Key", e);
        }

        return 0;
    }

    public async Task<IssueDataObject?> GetByIssueKeyAsync(string issueKey)
    {
        var issue = await this.issueRepository.GetByIdAsync(GetIdFromKey(issueKey));
        return issue?.Adapt<IssueDataObject>();
    }

    public async Task<(IEnumerable<IssueDataObject>, Pagination)> GetPagedAllByStatusAsync(
        string statusCategory, int pageNumber, int pageSize,
        IncludeOptions<Issue>? includeOptions = null)
    {
        var (issues, paginationMetadata) = (Enumerable.Empty<Issue>(), new Pagination());

        if (Enum.TryParse(statusCategory, ignoreCase: true, out IssueStatusCategory statusCategoryEnum))
        {
            switch (statusCategoryEnum)
            {
                case IssueStatusCategory.All:
                    (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                        pageNumber, pageSize, includeOptions: includeOptions);
                    break;
                case IssueStatusCategory.Open:
                    (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(
                        pageNumber, pageSize, i => i.IssueStatusId != (int)DomainEnums.IssueStatus.Resolved
                            && i.IssueStatusId != (int)DomainEnums.IssueStatus.Closed, includeOptions);
                    break;
                case IssueStatusCategory.Resolved:
                    (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(pageNumber, pageSize,
                        i => i.IssueStatusId == (int)DomainEnums.IssueStatus.Resolved, includeOptions);
                    break;
                case IssueStatusCategory.Closed:
                    (issues, paginationMetadata) = await this.issueRepository.GetPagedAllAsync(pageNumber, pageSize,
                        i => i.IssueStatusId == (int)DomainEnums.IssueStatus.Closed, includeOptions);
                    break;
            }

            // Use ICollection instead of IEnumerable to materialize object (required for Mapster)
            return (issues.Adapt<ICollection<IssueDataObject>>(), paginationMetadata);
        }

        throw new InvalidCastException($"Cannot cast string into enum. Value: {statusCategory}");
    }
}
