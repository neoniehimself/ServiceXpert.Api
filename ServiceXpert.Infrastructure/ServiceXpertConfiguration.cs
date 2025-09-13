using System.ComponentModel.DataAnnotations;

namespace ServiceXpert.Infrastructure;
public class ServiceXpertConfiguration
{
    [Required(ErrorMessage = "Fatal: Missing connection string")]
    public string ConnectionString { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fatal: Missing Jwt key")]
    public string JwtSecretKey { get; set; } = string.Empty;
}
