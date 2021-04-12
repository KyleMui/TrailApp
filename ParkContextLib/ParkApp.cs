using System;
using Microsoft.EntityFrameworkCore;
namespace Packt.Shared {
 public class ParkApp : DbContext {
 public DbSet<ParkTable> ParkTable { get; set; }
 public ParkApp(DbContextOptions<ParkApp> options) : base(options) { }
    protected override void OnModelCreating (
      ModelBuilder modelBuilder )
    {
      base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ParkTable>()
        .HasNoKey()
        .Property(c => c.Park_Name)
        .IsRequired()
        .HasMaxLength(30);

    }
 }

}
