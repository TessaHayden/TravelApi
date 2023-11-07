using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public DbSet<Travel> Travels { get; set; }
    public TravelApiContext(DbContextOptions<TravelApiContext> options) : base (options)
    {

    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Travel>()
            .HasData(
              new Travel { TravelId = 1, City = "Portland", Country = "USA", User_Name = "Mee" },
              new Travel { TravelId = 2, City = "Los Angeles", Country = "USA", User_Name = "Also Mee"}
            );
    }
  }
}