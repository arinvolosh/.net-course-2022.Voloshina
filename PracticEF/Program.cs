using Microsoft.EntityFrameworkCore;
using ModelsDb;

namespace HelloApp
{
    public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<ClientDb> clients  { get; set; }
        public DbSet<EmployeeDb> employees { get; set; }
        public DbSet<AccountDb> accounts { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder
        optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
            "Host=localhost;Port = 1234;Database = usersdb; Username = postgres;Password = 0000"
        );
        }
    }
}
