using Microsoft.EntityFrameworkCore;

namespace Capture_image.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        
        }

        public DbSet<Capture_image.Models.ImageModel> Images { get; set; }
    }
}
