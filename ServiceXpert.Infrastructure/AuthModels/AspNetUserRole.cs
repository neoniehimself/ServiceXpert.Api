using Microsoft.AspNetCore.Identity;
using ServiceXpert.Domain.Shared.Audits

namespace ServiceXpert.Infrastructure.AuthModels;
public class AspNetUserRole : IdentityUserRole<Guid>, IAudit
{
    public Guid CreatedByUserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? ModifyUserId { get; set; }

    public DateTime? ModifyDate { get; set; }
}
