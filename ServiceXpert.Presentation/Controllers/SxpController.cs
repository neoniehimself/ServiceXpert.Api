using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Presentation.Models;
using System.Net;

namespace ServiceXpert.Presentation.Controllers;
public class SxpController : ControllerBase
{
    [NonAction]
    protected IEnumerable<string> GetModelStateErrors() => this.ModelState.Values.SelectMany(modelStateEntry => modelStateEntry.Errors).Select(modelError => modelError.ErrorMessage);

    [NonAction]
    protected IActionResult BadRequestInvalidModelState()
    {
        return BadRequest(ApiResponse.Error(GetModelStateErrors(), status: HttpStatusCode.BadRequest));
    }

    [NonAction]
    protected IActionResult InternalServerError(object? value = null)
    {
        return StatusCode((int)HttpStatusCode.InternalServerError, value);
    }
}
