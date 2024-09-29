using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HubCommerce.Models
{
    public class FakeDbContext : IdentityDbContext
    {
        public FakeDbContext(DbContextOptions options) : base(options) { }
    }
}
