using Microsoft.EntityFrameworkCore;
using ParkingIoT2.Models.Domain;

namespace ParkingIoT2.Data
{
    public class ParkingIOTDBContext : DbContext
    {
        //Step 1: If the table don't exsist, please create the table
        public ParkingIOTDBContext(DbContextOptions<ParkingIOTDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RFIDCode>()
                .HasIndex(p => p.Code)
                .IsUnique();
            modelBuilder.Entity<RFIDCode>()
                .Property(p => p.Code)
                .IsRequired();
        }

        //Step 1: If the table don't exsist, please create the table
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ParkingArea> ParkingAreas { get; set; }
        public DbSet<ParkingSlot> ParkingSlots { get; set; }
        public DbSet<RFIDCode> RFIDCodes { get; set; }
        //Step 2: Create a connection string in the appsetting.json and config in Program.cs

    }
}
