using EventManagerPro.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagerPro.Data;

public class AppDbContext : DbContext
{
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<AttendanceRecord> AttendanceRecords => Set<AttendanceRecord>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=eventmanager.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<AttendanceRecord>()
            .HasIndex(a => new { a.EventId, a.ParticipantId })
            .IsUnique();
    }
}
