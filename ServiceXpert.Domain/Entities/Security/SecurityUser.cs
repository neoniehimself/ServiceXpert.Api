using Microsoft.AspNetCore.Identity;

namespace ServiceXpert.Domain.Entities.Security;
public class SecurityUser : IdentityUser<Guid>, IEntityBase
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public bool IsActive { get; set; }

    public SecurityProfile? SecurityProfile { get; set; }

    public SecurityUser()
    {
    }

    public SecurityUser(string userName) : base(userName)
    {
    }
}
