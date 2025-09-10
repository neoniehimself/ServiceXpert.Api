using Microsoft.AspNetCore.Identity;

namespace ServiceXpert.Infrastructure.AuthModels;
public class AspNetRole : IdentityRole<Guid>
{
    public AspNetRole()
    {
    }

    public AspNetRole(string roleName) : base(roleName)
    {
    }
}
