namespace ServiceXpert.Application.Models.Security.QueryOptions;
public class GetPagedUsersQueryOption
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

    private string? userName;

    public string? UserName
    {
        get => this.userName ?? string.Empty;
        set => this.userName = value;
    }

    private string? firstName;

    public string? FirstName
    {
        get => this.firstName ?? string.Empty;
        set => this.firstName = value;
    }

    private string? lastName;

    public string? LastName
    {
        get => this.lastName ?? string.Empty;
        set => this.lastName = value;
    }
}
