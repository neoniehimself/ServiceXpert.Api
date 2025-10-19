using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Enums;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Services.Concretes;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories.Issues;
using ServiceXpert.Domain.ValueObjects.Pagination;

namespace ServiceXpert.Application.Services.Concretes.Issues;
public class IssueService : ServiceBase<int, Issue, IssueDataObject>, IIssueService
{
    private readonly IIssueRepository issueRepository;

    public IssueService(IMapper mapper, IIssueRepository issueRepository) : base(mapper, issueRepository)
    {
        this.issueRepository = issueRepository;
    }

    public async Task<ServiceResult<PaginationResult<IssueDataObject>>> GetPagedIssuesByStatusAsync(string statusCategory, int pageNumber, int pageSize, IncludeOptions<Issue>? includeOptions = null)
    {
        var paginationResult = new PaginationResult<Issue>();

        if (Enum.TryParse(statusCategory, ignoreCase: true, out IssueStatusCategory statusCategoryEnum))
        {
            switch (statusCategoryEnum)
            {
                case IssueStatusCategory.All:
                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        pageNumber, pageSize, includeOptions: includeOptions);
                    break;
                case IssueStatusCategory.Open:
                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        pageNumber, pageSize, i => i.IssueStatusId != (int)Domain.Enums.Issues.IssueStatus.Resolved
                            && i.IssueStatusId != (int)Domain.Enums.Issues.IssueStatus.Closed, includeOptions);
                    break;
                case IssueStatusCategory.Resolved:
                    paginationResult = await this.issueRepository.GetPagedAllAsync(pageNumber, pageSize,
                        i => i.IssueStatusId == (int)Domain.Enums.Issues.IssueStatus.Resolved, includeOptions);
                    break;
                case IssueStatusCategory.Closed:
                    paginationResult = await this.issueRepository.GetPagedAllAsync(pageNumber, pageSize,
                        i => i.IssueStatusId == (int)Domain.Enums.Issues.IssueStatus.Closed, includeOptions);
                    break;
            }

            var paginationResultToReturn = new PaginationResult<IssueDataObject>(paginationResult.Items.Adapt<ICollection<IssueDataObject>>(), paginationResult.Pagination);
            return ServiceResult<PaginationResult<IssueDataObject>>.Ok(paginationResultToReturn);
        }

        return ServiceResult<PaginationResult<IssueDataObject>>.Fail(ServiceResultStatus.InternalError, [$"Invalid cast of string to enum. {nameof(statusCategory)}"]);
    }
}
