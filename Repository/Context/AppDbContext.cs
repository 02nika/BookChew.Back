using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Repository.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserPassword> Passwords { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UserConfig();
    }
}