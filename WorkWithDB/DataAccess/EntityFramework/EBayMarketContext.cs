using Microsoft.EntityFrameworkCore;
using WorkWithDB.DataAccess.EntityFramework.Entity;

namespace WorkWithDB.DataAccess.EntityFramework
{
    internal class EBayMarketContext : DbContext
    {
        public EBayMarketContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Market> Markets { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
