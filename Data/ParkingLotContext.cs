using Microsoft.EntityFrameworkCore;
using ParkingLotAPI.Models;

namespace ParkingLotAPI.Data
{
    public class ParkingLotContext : DbContext
    {
        public ParkingLotContext(DbContextOptions<ParkingLotContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}