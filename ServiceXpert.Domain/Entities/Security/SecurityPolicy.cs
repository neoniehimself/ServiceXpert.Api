namespace ServiceXpert.Domain.Entities.Security;
public class SecurityPolicy : EntityBase<int>
{
    public string Name { get; set; } = string.Empty;
}
