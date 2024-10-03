using Citas_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Citas_API.Data;

public class AppStoreContext(DbContextOptions<AppStoreContext> options) : DbContext(options)
{
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Specialty> Specialties =>  Set<Specialty>();
}
