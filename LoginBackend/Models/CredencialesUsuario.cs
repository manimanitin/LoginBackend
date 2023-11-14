using System.ComponentModel.DataAnnotations;

namespace LoginBackend.Models;

public class CredencialesUsuario
{
    [EmailAddress]
    public required string email { get; set; }
    public required string password { get; set; }
}
