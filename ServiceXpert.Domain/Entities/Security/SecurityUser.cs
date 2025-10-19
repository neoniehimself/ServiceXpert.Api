using Microsoft.AspNetCore.Identity;
using ServiceXpert.Domain.Audits;

namespace ServiceXpert.Domain.Entities.Security;
public class SecurityUser : IdentityUser<Guid>, IAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public SecurityProfile? SecurityProfile { get; set; }

    public SecurityUser()
    {
    }

    public SecurityUser(string userName) : base(userName)
    {
    }
}
