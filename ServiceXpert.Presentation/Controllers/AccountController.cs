using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.DataObjects;
using ServiceXpert.Application.Services.Contracts;

namespace ServiceXpert.Presentation.Controllers;
[Route("Api/Account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAspNetUserService aspNetUserService;

    public AccountController(
        IAspNetUserService aspNetUserService)
    {
        this.aspNetUserService = aspNetUserService;
    }

    [HttpPost(nameof(Register))]
    public async Task<ActionResult> Register(RegisterUserDataObject dataObject)
    {
        if (!this.ModelState.IsValid)
        {
            return BadRequest(this.ModelState);
        }

        var result = await this.aspNetUserService.RegisterAsync(dataObject);
        return result.Succeeded ? Ok("Register successful!") : BadRequest(result.Errors);
    }

    [HttpPost(nameof(Login))]
    public Task<ActionResult> Login(LoginUserDataObject dataObject)
    {
        throw new NotImplementedException();
    }

    [HttpPost(nameof(AddRole))]
    public Task<ActionResult> AddRole(string roleName)
    {
        throw new NotImplementedException();
    }

    [HttpPost(nameof(AssignRole))]
    public Task<ActionResult> AssignRole(UserRoleDataObject dataObject)
    {
        throw new NotImplementedException();
    }
}
