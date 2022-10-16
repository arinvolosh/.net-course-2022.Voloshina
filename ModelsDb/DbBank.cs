using Microsoft.EntityFrameworkCore;

namespace ModelsDb
{
    public class DbBank: DbContext
    {
        public DbSet<ClientDb> clients { get; set; }
        public DbSet<EmployeeDb> employees { get; set; }
        public DbSet<AccountDb> accounts { get; set; }
        public DbSet<CurrencyDb> currencies { get; set; }
        public DbBank()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
            "Host=localhost;Port = 1234;Database = bankdb; Username = postgres;Password = 0000"
        );
        }
    }
}
