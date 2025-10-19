using Microsoft.AspNetCore.Mvc;
using ServiceXpert.Application.Enums;
using ServiceXpert.Application.Models;
using System.Net;

namespace ServiceXpert.Presentation.Controllers;
public class SxpController : ControllerBase
{
    [NonAction]
    protected IEnumerable<string> GetModelStateErrors() => this.ModelState.Values.SelectMany(modelStateEntry => modelStateEntry.Errors).Select(modelError => modelError.ErrorMessage);

    [NonAction]
    protected IActionResult BadRequestInvalidModelState() => BadRequest(Models.ApiResponse.Fail(HttpStatusCode.BadRequest, GetModelStateErrors()));

    [NonAction]
    protected IActionResult ApiResponse(ServiceResult result)
    {
        if (!result.IsSuccess)
        {
            return result.Status switch
            {
                ServiceResultStatus.ValidationError => BadRequest(Models.ApiResponse.Fail(HttpStatusCode.BadRequest, result.Errors)),
                ServiceResultStatus.Unauthorized => Unauthorized(Models.ApiResponse.Fail(HttpStatusCode.Unauthorized, result.Errors)),
                ServiceResultStatus.NotFound => NotFound(Models.ApiResponse.Fail(HttpStatusCode.NotFound, result.Errors)),
                _ => InternalServerError(result.Errors)
            };
        }

        return Ok(Models.ApiResponse.Ok());
    }

    [NonAction]
    protected IActionResult ApiResponse<T>(ServiceResult<T> result)
    {
        if (!result.IsSuccess)
        {
            return result.Status switch
            {
                ServiceResultStatus.ValidationError => BadRequest(Models.ApiResponse<T>.Fail(HttpStatusCode.BadRequest, result.Errors)),
                ServiceResultStatus.Unauthorized => Unauthorized(Models.ApiResponse<T>.Fail(HttpStatusCode.Unauthorized, result.Errors)),
                ServiceResultStatus.NotFound => NotFound(Models.ApiResponse<T>.Fail(HttpStatusCode.NotFound, result.Errors)),
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
