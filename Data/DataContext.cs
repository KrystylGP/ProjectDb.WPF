using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<ProjectEntity> Projects { get; set; } // Lägger till 'Projects'-tabellen.
    public DbSet<CustomerEntity> Customers { get; set; } // Lägger till 'Customers'-tabellen.

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.CustomerInfo)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade); // Om en kund tas bort, raderas även dess projekt.
    }
}