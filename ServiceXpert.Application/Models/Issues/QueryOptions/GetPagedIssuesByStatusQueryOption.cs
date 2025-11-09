using ServiceXpert.Application.Enums;

namespace ServiceXpert.Application.Models.Issues.QueryOptions;
public class GetPagedIssuesQueryOption
{
    public string IssueKey { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string StatusCategory { get; set; } = IssueStatusCategory.All.ToString();

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
