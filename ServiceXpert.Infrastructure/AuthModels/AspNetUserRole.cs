using Microsoft.AspNetCore.Identity;

namespace ServiceXpert.Infrastructure.AuthModels;
public class AspNetUserRole : IdentityUserRole<Guid>
{
    public DateTime CreateDate { get; set; }
}
