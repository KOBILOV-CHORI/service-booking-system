using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<WorkingHours> WorkingHours { get; set; }
    public DbSet<Break> Breaks { get; set; }
    public DbSet<SpecialBreak> SpecialBreaks { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ClosedDay> ClosedDays { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Code)
            .IsUnique();

        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<City>()
            .HasIndex(c => new { c.Name, c.CountryId })
            .IsUnique();

        modelBuilder.Entity<Category>()
            .HasIndex(c => c.Name)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();
    }
}