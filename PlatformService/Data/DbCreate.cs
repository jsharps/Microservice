using PlatformService.Models;

namespace PlatformService.Data
{
  public static class DbCreate
  {
    public static void PrePopulation(IApplicationBuilder applicationBuilder)
    {
      using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
      {
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
      }

    }

    private static void SeedData(AppDbContext context)
    {
      if (!context.Platforms.Any())
      {
        Console.WriteLine("--> Seeding data");

        context.Platforms.AddRange(
          new Platform() {Name="Dot net", Publisher="Microsft", Cost="Free"},
          new Platform() {Name="SQL Server Express", Publisher="Microsft", Cost="Free"},
          new Platform() {Name="kubernetes", Publisher="Cloud Native Computing Foundation", Cost="Free"}
        );

        context.SaveChanges();
      }
      else
      {
        Console.WriteLine("--> We akready have data");
      }
    }
  }
}