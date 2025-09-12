using Microsoft.AspNetCore.Identity;
using ServiceXpert.Domain.Shared.Audits;

namespace ServiceXpert.Infrastructure.AuthModels;
public class AspNetUser : IdentityUser<Guid>, IDate
{
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;

    public DateTime ModifyDate { get; set; } = DateTime.UtcNow;

    public AspNetUser()
    {
    }

    public AspNetUser(string userName) : base(userName)
    {
    }
}
