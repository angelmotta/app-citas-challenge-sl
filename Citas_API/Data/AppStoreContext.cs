using Citas_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Citas_API.Data;

public class AppStoreContext(DbContextOptions<AppStoreContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Specialty> Specialties =>  Set<Specialty>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Specialty>().HasData(
            new {Id = 1, Name = "General"},
            new {Id = 2, Name = "Dermatología"},
            new {Id = 3, Name = "Cardiología"},
            new {Id = 4, Name = "Pediatría"}
        );
    }
}
