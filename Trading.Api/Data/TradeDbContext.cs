using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Trading.Api.Models;

namespace Trading.Api.Data
{
    public class TradeDbContext : DbContext
    {
        public TradeDbContext(DbContextOptions<TradeDbContext> options) : base(options) { }

        public DbSet<Trade> Trades { get; set; }
    }

    public class TradeDbContextFactory : IDesignTimeDbContextFactory<TradeDbContext>
    {
        public TradeDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<TradeDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new TradeDbContext(optionsBuilder.Options);
        }
    }
}
