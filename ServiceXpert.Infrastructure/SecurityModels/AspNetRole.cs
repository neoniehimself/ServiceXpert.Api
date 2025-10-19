using Microsoft.AspNetCore.Identity;
using ServiceXpert.Domain.Audits;

namespace ServiceXpert.Infrastructure.SecurityModels;
public class AspNetRole : IdentityRole<Guid>, IAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTimeOffset? ModifiedDate { get; set; }

    public AspNetRole()
    {
    }

    public AspNetRole(string roleName) : base(roleName)
    {
    }
}
