using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_Api_Try.Data;
using Web_Api_Try.Models;

namespace Web_Api_Try.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
        
    {
        private readonly WeatherContext _weatherContext;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private void summaryCalculator(WeatherForecast forecast)
        {
            int temp = forecast.TemperatureC;

            if(temp<0)
                forecast.Summary = Summaries.ElementAt(0);
            else if (temp >= 0 && temp < 10)
                forecast.Summary = Summaries.ElementAt(1);
            else if (temp >= 10 && temp < 15)
                forecast.Summary = Summaries.ElementAt(2);
            else if (temp >= 15 && temp < 20)
                forecast.Summary = Summaries.ElementAt(3);
            else if (temp >= 20 && temp < 25)
                forecast.Summary = Summaries.ElementAt(4);
            else if (temp >= 25 && temp < 30)
                forecast.Summary = Summaries.ElementAt(5);
            else if (temp >= 30 && temp < 35)
                forecast.Summary = Summaries.ElementAt(6);
            else if (temp >= 35 && temp < 40)
                forecast.Summary = Summaries.ElementAt(7);
            else if (temp >= 40 && temp < 45)
                forecast.Summary = Summaries.ElementAt(8);
            else
                forecast.Summary = Summaries.ElementAt(9);
        }

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherContext weatherContext)
        {
            _logger = logger;
            _weatherContext = weatherContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecasts()
        {
            return await _weatherContext.Forecasts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecastById(int id)
        {
            var weatherForecast = await _weatherContext.Forecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> CreateWeatherForecast([FromBody] WeatherForecast weatherForecast)
        {
            _weatherContext.Forecasts.Add(weatherForecast);
            summaryCalculator(weatherForecast);
            await _weatherContext.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetWeatherForecastById), new { id = weatherForecast.id }, weatherForecast);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeatherForecast(int id, [FromBody] WeatherForecast weatherForecast)
        {
            var existingWeatherForecast = await _weatherContext.Forecasts.FindAsync(id);

            if (existingWeatherForecast == null)
            {
                return NotFound();
            }

            existingWeatherForecast.Date = weatherForecast.Date;
            existingWeatherForecast.TemperatureC = weatherForecast.TemperatureC;

            summaryCalculator(weatherForecast);

            await _weatherContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await _weatherContext.Forecasts.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            _weatherContext.Forecasts.Remove(weatherForecast);
            await _weatherContext.SaveChangesAsync();

            return NoContent();
        }
    }
}