using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Shared;
using ServiceXpert.Application.Shared.Enums;
using System.Net;

namespace ServiceXpert.Presentation.Controllers;
public class SxpController : ControllerBase
{
    [NonAction]
    protected IEnumerable<string> GetModelStateErrors() => this.ModelState.Values.SelectMany(modelStateEntry => modelStateEntry.Errors).Select(modelError => modelError.ErrorMessage);

    [NonAction]
    protected IActionResult BadRequestInvalidModelState() => BadRequest(Models.ApiResponse.Fail(HttpStatusCode.BadRequest, GetModelStateErrors()));

    [NonAction]
    protected IActionResult ApiResponse(Result result)
    {
        if (!result.IsSuccess)
        {
            return result.Status switch
            {
                ResultStatus.ValidationError => BadRequest(Models.ApiResponse.Fail(HttpStatusCode.BadRequest, result.Errors)),
                ResultStatus.Unauthorized => Unauthorized(Models.ApiResponse.Fail(HttpStatusCode.Unauthorized, result.Errors)),
                ResultStatus.NotFound => NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, result.Errors)),
                _ => InternalServerError(result.Errors)
            };
        }

        return Ok(Models.ApiResponse.Ok());
    }

    [NonAction]
    protected IActionResult ApiResponse<T>(Result<T> result)
    {
        if (!result.IsSuccess)
        {
            return result.Status switch
            {
                ResultStatus.ValidationError => BadRequest(Models.ApiResponse<T>.Fail(HttpStatusCode.BadRequest, result.Errors)),
                ResultStatus.Unauthorized => Unauthorized(Models.ApiResponse<T>.Fail(HttpStatusCode.Unauthorized, result.Errors)),
                ResultStatus.NotFound => NotFound(Models.ApiResponse<T>.Fail(HttpStatusCode.NotFound, result.Errors)),
                _ => InternalServerError<T>(result.Errors)
            };
        }

        return Ok(Models.ApiResponse<T>.Ok(result.Value));
    }

    [NonAction]
    protected IActionResult InternalServerError(IEnumerable<string> errors)
    {
        return base.StatusCode((int)HttpStatusCode.InternalServerError,
            Models.ApiResponse.Fail(HttpStatusCode.InternalServerError, errors));
    }

    [NonAction]
    protected IActionResult InternalServerError<T>(IEnumerable<string> errors)
    {
        return base.StatusCode((int)HttpStatusCode.InternalServerError,
            Models.ApiResponse<T>.Fail(HttpStatusCode.InternalServerError, errors));
    }
}
