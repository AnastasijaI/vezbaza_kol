using Microsoft.EntityFrameworkCore;
using vezba1.Models;

namespace vezba1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Avtor> Avtori {  get; set; }
        public DbSet<Kniga> Knigi { get; set; }
        public DbSet<AvtorKniga> AvtorKnigi { get; set; }
    }
}
