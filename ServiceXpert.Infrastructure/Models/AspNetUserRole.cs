using Microsoft.AspNetCore.Identity;

namespace ServiceXpert.Infrastructure.Models;
public class AspNetUserRole : IdentityUserRole<Guid>
{
    public DateTime CreateDate { get; set; }
}
