using Microsoft.EntityFrameworkCore;

namespace ToaPro.Models
{
    public class IntexContext : DbContext
    {
        public IntexContext(DbContextOptions<IntexContext> options)
            : base(options) { }
        public DbSet<Student> Students { get; set; }
    }
}
