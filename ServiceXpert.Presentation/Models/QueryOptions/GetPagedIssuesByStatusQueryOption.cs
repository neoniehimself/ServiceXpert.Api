namespace ServiceXpert.Presentation.Models.QueryOptions;
public class GetPagedIssuesByStatusQueryOption
{
    public string StatusCategory { get; set; } = "All";

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public bool IncludeComments { get; set; } = false;

    public bool IncludeCreatedByUser { get; set; } = false;

    public bool IncludeAssignee { get; set; } = false;
}
