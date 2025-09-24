namespace ServiceXpert.Application.Shared.Enums;
public enum ResultStatus
{
    Success = 200,
    ValidationError = 400,
    Unauthorized = 401,
    NotFound = 404,
    InternalError = 500
}
