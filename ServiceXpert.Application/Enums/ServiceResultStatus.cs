namespace ServiceXpert.Application.Enums;
public enum ServiceResultStatus
{
    Success = 200,
    ValidationError = 400,
    Unauthorized = 401,
    NotFound = 404,
    InternalError = 500
}
