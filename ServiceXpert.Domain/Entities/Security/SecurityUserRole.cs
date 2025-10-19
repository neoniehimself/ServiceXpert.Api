using Microsoft.AspNetCore.Identity;
using ServiceXpert.Domain.Audits;

namespace ServiceXpert.Domain.Entities.Security;
public class SecurityUserRole : IdentityUserRole<Guid>, IAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }
}
