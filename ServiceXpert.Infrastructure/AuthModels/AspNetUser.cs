using Microsoft.AspNetCore.Identity;
using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Infrastructure.AuthModels;
public class AspNetUser : IdentityUser<Guid>, IAuditable
{
    public Guid CreateUserId { get; set; }

    public DateTime CreateDate { get; set; }

    public Guid ModifyUserId { get; set; }

    public DateTime ModifyDate { get; set; }

    public AspNetUser()
    {
    }

    public AspNetUser(string userName) : base(userName)
    {
    }
}
