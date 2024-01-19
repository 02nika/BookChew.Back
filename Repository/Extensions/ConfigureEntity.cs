using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Extensions;

public static class ConfigureEntity
{
    public static void UserConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<User>()
            .Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(100);
        
        modelBuilder.Entity<User>()
            .Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(100);
        
        modelBuilder.Entity<User>()
            .Property(p => p.UserName)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<User>()
            .Property(p => p.PersonalNumber)
            .HasMaxLength(20);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Passwords)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Restaurants)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);
    }

    public static void RestaurantConfig(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<Restaurant>()
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}