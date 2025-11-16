using Microsoft.AspNetCore.Identity;

namespace ServiceXpert.Domain.Entities.Security;
public class SecurityUserLogin : IdentityUserLogin<Guid>, IEntityBase
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
