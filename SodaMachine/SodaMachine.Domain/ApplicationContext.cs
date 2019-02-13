using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SodaMachine.Domain.Models;

namespace SodaMachine.Domain
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Soda> Soda { get; set; }
        public DbSet<Coin> Coin { get; set; }
        public DbSet<CoinsInMachine> CoinsInMachine { get; set; }
        private readonly IConfiguration _configuration;

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = _configuration.GetSection("ConnString").Value;
            optionsBuilder.UseSqlServer(conn);
        }
    }
}
