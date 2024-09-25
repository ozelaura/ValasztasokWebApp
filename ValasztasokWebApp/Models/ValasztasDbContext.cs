using Microsoft.EntityFrameworkCore;

namespace ValasztasokWebApp.Models
{
    public class ValasztasDbContext : DbContext
    {
        public ValasztasDbContext(DbContextOptions<ValasztasDbContext> options)
            : base(options) 
        {

        }

        public DbSet<Jelolt> Jeloltek {  get; set; }
        public DbSet<Part> Partok { get; set; }
    }
}
