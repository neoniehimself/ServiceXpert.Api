using Microsoft.AspNetCore.Identity;
using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Infrastructure.SecurityModels;
public class AspNetUserRole : IdentityUserRole<Guid>, IAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifiedByUserId { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
