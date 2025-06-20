using Microsoft.EntityFrameworkCore;

namespace JobApplicationApi.Models
{
    public class AppDBContext(DbContextOptions<AppDBContext> options) : DbContext(options)
    {
        public DbSet<Application> JobApplication { get; set; }
    }
}
