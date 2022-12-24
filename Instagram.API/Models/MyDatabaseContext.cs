using Microsoft.EntityFrameworkCore;

namespace MyProject.Models
{
    public class MyDatabaseContext : DbContext
    {
        public MyDatabaseContext(DbContextOptions options) : base(options) { }
        DbSet<User> Users
        {
            get;
            set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ConStr");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
