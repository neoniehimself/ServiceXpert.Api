namespace ServiceXpert.Presentation.Models.QueryOptions;
public class GetPagedIssuesByStatusQueryOption
{
    public string StatusCategory { get; set; } = "All";

    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
