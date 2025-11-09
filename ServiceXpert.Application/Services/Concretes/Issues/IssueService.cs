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
using ServiceXpert.Domain.Helpers.Persistence.Includes;
using ServiceXpert.Domain.Repositories.Issues;
using ServiceXpert.Domain.ValueObjects.Pagination;

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

    public override Task<ServiceResult<IEnumerable<IssueDataObject>>> GetAllAsync(IncludeOptions<Issue>? includeOptions = null, CancellationToken cancellationToken = default)
    {
        includeOptions ??= new IncludeOptions<Issue>();
        includeOptions.AddRange(GetRequiredNavigations());

        return base.GetAllAsync(includeOptions, cancellationToken);
    }

    public override Task<ServiceResult<IssueDataObject>> GetByIdAsync(int id, IncludeOptions<Issue>? includeOptions = null, CancellationToken cancellationToken = default)
    {
        includeOptions ??= new IncludeOptions<Issue>();
        includeOptions.AddRange(GetRequiredNavigations());

        return base.GetByIdAsync(id, includeOptions, cancellationToken);
    }

    private ExpressionStarter<Issue> ConfigureGetPagedIssuesQueryOptionFilters(GetPagedIssuesQueryOption queryOption)
    {
        var filters = PredicateBuilder.New<Issue>(true);

        if (!string.IsNullOrEmpty(queryOption.IssueKey))
        {
            var issueId = IssueUtil.GetIdFromKey(queryOption.IssueKey);
            filters = filters.And(i => i.Id == issueId);
        }

        if (!string.IsNullOrEmpty(queryOption.Name))
        {
            filters = filters.And(i => i.Name.ToLower() == queryOption.Name.ToLower());
        }

        return filters;
    }

    public async Task<ServiceResult<PaginationResult<IssueDataObject>>> GetPagedIssuesAsync(GetPagedIssuesQueryOption? queryOption = null, IncludeOptions<Issue>? includeOptions = null, CancellationToken cancellationToken = default)
    {
        queryOption ??= new GetPagedIssuesQueryOption();
        includeOptions ??= new IncludeOptions<Issue>();
        includeOptions.AddRange(GetRequiredNavigations());

        var paginationResult = new PaginationResult<Issue>();
        var filters = ConfigureGetPagedIssuesQueryOptionFilters(queryOption);

        try
        {
            var statusCategory = queryOption.StatusCategory.ToEnum<IssueStatusCategory>();
            switch (statusCategory)
            {
                case IssueStatusCategory.All:
                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        queryOption.PageNumber,
                        queryOption.PageSize,
                        filters,
                        includeOptions,
                        cancellationToken);
                    break;
                case IssueStatusCategory.Open:
                    filters = filters.And(i =>
                        i.IssueStatusId != Domain.Enums.Issues.IssueStatus.Resolved.ToInt()
                        && i.IssueStatusId != Domain.Enums.Issues.IssueStatus.Closed.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        queryOption.PageNumber,
                        queryOption.PageSize,
                        filters,
                        includeOptions,
                        cancellationToken);
                    break;
                case IssueStatusCategory.Resolved:
                    filters = filters.And(i => i.IssueStatusId == Domain.Enums.Issues.IssueStatus.Resolved.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        queryOption.PageNumber,
                        queryOption.PageSize,
                        filters,
                        includeOptions,
                        cancellationToken);
                    break;
                case IssueStatusCategory.Closed:
                    filters = filters.And(i => i.IssueStatusId == Domain.Enums.Issues.IssueStatus.Closed.ToInt());

                    paginationResult = await this.issueRepository.GetPagedAllAsync(
                        queryOption.PageNumber,
                        queryOption.PageSize,
                        filters,
                        includeOptions,
                        cancellationToken);
                    break;
            }

            var paginationResultToReturn = new PaginationResult<IssueDataObject>(paginationResult.Items.Adapt<ICollection<IssueDataObject>>(), paginationResult.Pagination);
            return ServiceResult<PaginationResult<IssueDataObject>>.Ok(paginationResultToReturn);
        }
        catch (Exception e)
        {
            return ServiceResult<PaginationResult<IssueDataObject>>.Fail(ServiceResultStatus.InternalError, [e.Message]);
        }
    }
}
