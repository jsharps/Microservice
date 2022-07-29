using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
  public static class DbCreate
  {
    public static void PrePopulation(IApplicationBuilder applicationBuilder, bool isProd)
    {
      using(var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
      {
        SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProd);
      }

    }

    private static void SeedData(AppDbContext context,  bool isProd)
    {
      if(isProd)
      {
        try
        {
          Console.WriteLine($"--> Attempting Migration");
          context.Database.Migrate();
        }
        catch(Exception ex)
        {
          Console.WriteLine($"--> Error. {ex}");
        }
      }


      if (!context.Platforms.Any())
      {
        Console.WriteLine("--> Seeding data");

        context.Platforms.AddRange(
          new Platform() {Name="Dot net", Publisher="Microsoft", Cost="Free"},
          new Platform() {Name="SQL Server Express", Publisher="Microsoft", Cost="Free"},
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