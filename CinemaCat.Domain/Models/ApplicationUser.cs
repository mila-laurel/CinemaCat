namespace CinemaCat.Domain.Models;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string[] Roles { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
}
