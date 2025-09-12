using Microsoft.AspNetCore.Identity;
using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Infrastructure.AuthModels;
public class AspNetUserRole : IdentityUserRole<Guid>, IDate
{
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public DateTime ModifyDate { get; set; } = DateTime.UtcNow;
}
