using jobPortal.Models;
using jobPortal.Models.job;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jobPortal.data
{
    public class ApplicationDbContext :IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<JobModel> JobModels { get; set; }
    }
}
