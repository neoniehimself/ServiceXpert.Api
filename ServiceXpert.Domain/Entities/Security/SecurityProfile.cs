namespace ServiceXpert.Domain.Entities.Security;
public class SecurityProfile : EntityBase<Guid>
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
