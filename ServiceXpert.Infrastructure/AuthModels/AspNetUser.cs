using Microsoft.AspNetCore.Identity;

namespace ServiceXpert.Infrastructure.AuthModels;
public class AspNetUser : IdentityUser<Guid>
{
    public AspNetUser()
    {
    }

    public AspNetUser(string userName) : base(userName)
    {
    }
}
