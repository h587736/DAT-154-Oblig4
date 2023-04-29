using Microsoft.EntityFrameworkCore;
using Oblig4_webapp.Models;

namespace Oblig4_webapp.MyDbContext
{
    public class MyDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the model here
        }

    }
}
