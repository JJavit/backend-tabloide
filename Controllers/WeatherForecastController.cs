using Microsoft.AspNetCore.Mvc;

namespace tabloidetek_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("DameHolaMundo")]
        public IActionResult Mensaje()
        {
            return Ok("Hola Mundo");
        }

        [HttpGet("Bienvenida")]
        public IActionResult DameBienvenida(string nombre)
        {
            return Ok("Hola " + nombre);
        }

        [HttpGet("Suma de dos numeros")]
        public IActionResult Suma(int num1, int num2)
        {
            int res = num1 + num2;
            return Ok("El resultado de la suma es: " + res);
        }
    }
}
