using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginBackend;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions option) : base(option)
    {
    }

}
