using Microsoft.EntityFrameworkCore;
using Web_Api_Try.Data;

namespace Web_Api_Try.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new WeatherContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<WeatherContext>>()))
            {
                if (context.Forecasts.Any())
                {
                    context.Database.ExecuteSqlRaw("TRUNCATE TABLE Forecasts");
                }
                context.Forecasts.AddRange(
                    new WeatherForecast
                    {
                        TemperatureC= 30,
                        Summary="Balmy"
                    },
                    new WeatherForecast
                    {
                        TemperatureC = 70,
                        Summary="Scorching"
                    }              
                );
                context.SaveChanges();
            }
        }
    }
}
