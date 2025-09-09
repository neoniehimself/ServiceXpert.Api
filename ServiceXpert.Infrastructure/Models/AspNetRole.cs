using Microsoft.AspNetCore.Identity;

namespace ServiceXpert.Infrastructure.Models;
public class AspNetRole : IdentityRole<Guid>
{
    public AspNetRole()
    {
    }

    public AspNetRole(string roleName) : base(roleName)
    {
    }
}
