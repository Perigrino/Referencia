using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RefsGuy.Application.Models;

namespace RefsGuy.Application.Database;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {
        modelBuilder.Entity<Users>()
            .HasMany(u => u.ReferralCode)
            .WithOne(w => w.Users)
            .HasForeignKey(w => w.UserId);
    }
    
    public DbSet<ReferralCode> ReferralCodes { get; set; }
    public DbSet<Users> Users { get; set; }

}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Assuming the appsettings.json file is in the same directory
            .AddJsonFile(@Directory.GetCurrentDirectory() + "/../RefsGuy.API/appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("Default");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}