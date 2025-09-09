using Microsoft.AspNetCore.Identity;

namespace ServiceXpert.Infrastructure.Models;
public class AspNetUser : IdentityUser<Guid>
{
    public AspNetUser()
    {
        this.AspNetRoles = [];
    }

    public AspNetUser(string userName) : base(userName)
    {
        this.AspNetRoles = [];
    }

    public virtual List<AspNetRole> AspNetRoles { get; set; }
}
