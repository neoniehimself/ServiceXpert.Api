using ServiceXpert.Application.Enums;

namespace ServiceXpert.Application.Models.Issues.QueryOptions;
public class GetPagedIssuesQueryOption
{
    private int? pageNumber;

    public int? PageNumber
    {
        get => this.pageNumber ?? 1;
        set => this.pageNumber = value;
    }

    private int? pageSize;

    public int? PageSize
    {
        get => this.pageSize ?? 10;
        set => this.pageSize = value;
    }

    private string? issueKey;

    public string? IssueKey
    {
        get => this.issueKey ?? string.Empty;
        set => this.issueKey = value;
    }

    private string? name;

    public string? Name
    {
        get => this.name ?? string.Empty;
        set => this.name = value;
    }

    private string? statusCategory;

    public string? StatusCategory
    {
        get => this.statusCategory ?? IssueStatusCategory.All.ToString();
        set => this.statusCategory = value;
    }
}
