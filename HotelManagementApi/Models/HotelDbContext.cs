using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Models
{
    public class HotelDbContext:DbContext
    {
        public HotelDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RegisterUser> RegisterUsers { get; set; }

    }
}
