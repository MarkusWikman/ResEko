using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ResEko.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :
        base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
