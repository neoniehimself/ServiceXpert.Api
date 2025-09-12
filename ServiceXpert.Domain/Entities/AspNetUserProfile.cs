namespace ServiceXpert.Domain.Entities;
public class AspNetUserProfile : EntityBase
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;
}
