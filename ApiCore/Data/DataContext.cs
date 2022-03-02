using Microsoft.EntityFrameworkCore;

namespace ApiCore.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Users> users { get; set; }

        public DbSet<Recibos> recibos { get; set; }
    }
}
