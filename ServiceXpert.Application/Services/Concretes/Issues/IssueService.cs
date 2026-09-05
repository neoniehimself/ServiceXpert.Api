using LinqKit;
using Mapster;
using MapsterMapper;
using ServiceXpert.Application.DataObjects.Issues;
using ServiceXpert.Application.Enums;
using ServiceXpert.Application.Extensions;
using ServiceXpert.Application.Models;
using ServiceXpert.Application.Models.Issues.QueryOptions;
using ServiceXpert.Application.Services.Contracts.Issues;
using ServiceXpert.Application.Utils;
using ServiceXpert.Domain.Entities.Issues;
using ServiceXpert.Domain.Helpers.Persistence;
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories.Issues;
using ServiceXpert.Domain.ValueObjects.Pagination;
using IssueStatusEnum = ServiceXpert.Domain.Enums.Issues.IssueStatus;

namespace ServiceXpert.Application.Services.Concretes.Issues;

internal class IssueService : ServiceBase<int, Issue, IssueDataObject>, IIssueService
{
    private readonly IIssueRepository issueRepository;

    public IssueService(IMapper mapper, IIssueRepository issueRepository) : base(mapper, issueRepository)
    {
        this.issueRepository = issueRepository;
    }

    private static IncludeExpressions<Issue> GetRequiredNavigations()
    {
        return
        [
            i => i.Reporter!,
            i => i.Reporter!.SecurityProfile!,
            i => i.Assignee!,
            i => i.Assignee!.SecurityProfile!
        ];
    }

    public override Task<ServiceResult<IssueDataObject>> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(id, new IncludeOption<Issue>(GetRequiredNavigations()), cancellationToken);
    }

    private static ExpressionStarter<Issue> GetFiltersFromGetPagedIssuesQueryOptionFilters(GetPagedIssuesQueryOption queryOption)
    {
        var filters = PredicateBuilder.New<Issue>(true);

        if (!string.IsNullOrEmpty(queryOption.IssueKey?.Trim()))
        {
            var issueId = IssueUtil.GetIdFromKey(queryOption.IssueKey);
            filters = filters.And(i => i.Id == issueId);
        }

        if (!string.IsNullOrEmpty(queryOption.Name?.Trim()))
        {
            filters = filters.And(i => i.Name.Contains(queryOption.Name));
        }

        return filters;
    }

    public async Task<ServiceResult<PaginationResult<IssueDataObject>>> GetPagedIssuesAsync(GetPagedIssuesQueryOption queryOption, CancellationToken cancellationToken = default)
    {
        var paginationResult = new PaginationResult<Issue>();
        var filters = GetFiltersFromGetPagedIssuesQueryOptionFilters(queryOption);

        try
        {
            var statusCategory = queryOption.StatusCategory?.ToEnum<IssueStatusCategory>();
            switch (statusCategory)
            {
                case IssueStatusCategory.All:
                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        cancellationToken);
                    break;

                case IssueStatusCategory.Open:
                    filters = filters.And(i =>
                        i.IssueStatusId != IssueStatusEnum.Resolved.ToInt()
                        && i.IssueStatusId != IssueStatusEnum.Closed.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        cancellationToken);
                    break;

                case IssueStatusCategory.Resolved:
                    filters = filters.And(i => i.IssueStatusId == IssueStatusEnum.Resolved.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        cancellationToken);
                    break;

                case IssueStatusCategory.Closed:
                    filters = filters.And(i => i.IssueStatusId == IssueStatusEnum.Closed.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        cancellationToken);
                    break;
            }

            var paginationResultToReturn = new PaginationResult<IssueDataObject>(
                paginationResult.Items.Adapt<ICollection<IssueDataObject>>(),
                paginationResult.Pagination);

            return ServiceResult<PaginationResult<IssueDataObject>>.Ok(paginationResultToReturn);
        }
        catch (Exception e)
        {
            return ServiceResult<PaginationResult<IssueDataObject>>.Fail(ServiceResultStatus.InternalError, [e.Message]);
        }
    }
}
