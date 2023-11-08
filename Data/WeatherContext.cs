using Microsoft.EntityFrameworkCore;
using Web_Api_Try.Models;

namespace Web_Api_Try.Data
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options)
        : base(options)
        {
        }

        public DbSet<WeatherForecast> Forecasts { get; set; }
    }
}
