using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Library.Models;

namespace Oblig4_webapp.MyDbContext
{
    public class HotelDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rooms> Room { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the model here
        }
    }
}
