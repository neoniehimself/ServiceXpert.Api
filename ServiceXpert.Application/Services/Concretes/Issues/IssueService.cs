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
using EnumsOfIssue = ServiceXpert.Domain.Enums.Issues;

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

    public override Task<ServiceResult<IEnumerable<IssueDataObject>>> GetAllAsync(IncludeOption<Issue> includeOption, CancellationToken cancellationToken = default)
    {
        includeOption.AddRange(GetRequiredNavigations());
        return base.GetAllAsync(includeOption, cancellationToken);
    }

    public override Task<ServiceResult<IssueDataObject>> GetByIdAsync(int id, IncludeOption<Issue> includeOption, CancellationToken cancellationToken = default)
    {
        includeOption.AddRange(GetRequiredNavigations());
        return base.GetByIdAsync(id, includeOption, cancellationToken);
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
                        i.IssueStatusId != EnumsOfIssue.IssueStatus.Resolved.ToInt()
                        && i.IssueStatusId != EnumsOfIssue.IssueStatus.Closed.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        cancellationToken);
                    break;

                case IssueStatusCategory.Resolved:
                    filters = filters.And(i => i.IssueStatusId == EnumsOfIssue.IssueStatus.Resolved.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        cancellationToken);
                    break;

                case IssueStatusCategory.Closed:
                    filters = filters.And(i => i.IssueStatusId == EnumsOfIssue.IssueStatus.Closed.ToInt());

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

    public async Task<ServiceResult<PaginationResult<IssueDataObject>>> GetPagedIssuesAsync(GetPagedIssuesQueryOption queryOption, IncludeOption<Issue> includeOption, CancellationToken cancellationToken = default)
    {
        var paginationResult = new PaginationResult<Issue>();
        var filters = GetFiltersFromGetPagedIssuesQueryOptionFilters(queryOption);
        includeOption.AddRange(GetRequiredNavigations());

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
                        includeOption,
                        cancellationToken);
                    break;

                case IssueStatusCategory.Open:
                    filters = filters.And(i =>
                        i.IssueStatusId != EnumsOfIssue.IssueStatus.Resolved.ToInt()
                        && i.IssueStatusId != EnumsOfIssue.IssueStatus.Closed.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        includeOption,
                        cancellationToken);
                    break;

                case IssueStatusCategory.Resolved:
                    filters = filters.And(i => i.IssueStatusId == EnumsOfIssue.IssueStatus.Resolved.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        includeOption,
                        cancellationToken);
                    break;

                case IssueStatusCategory.Closed:
                    filters = filters.And(i => i.IssueStatusId == EnumsOfIssue.IssueStatus.Closed.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        (int)queryOption.PageNumber!,
                        (int)queryOption.PageSize!,
                        new FilterOption<Issue>(filters),
                        includeOption,
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
