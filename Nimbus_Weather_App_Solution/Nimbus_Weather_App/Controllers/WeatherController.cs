using Microsoft.AspNetCore.Mvc;
using Nimbus_Weather_App.Interfaces;
using Nimbus_Weather_App.Services;

namespace Nimbus_Weather_App.Controllers
{

    [Route("api/[controller]")]
	[ApiController]
	public class WeatherController : ControllerBase
	{

		private readonly IWeatherService _weatherService;

		public WeatherController(IWeatherService service)
		{
			_weatherService = service;
			service.Location = "London";
		}

        [HttpGet("{location}")] // GET /api/weather/london
		public async Task<IActionResult> Get(string location)
		{
			_weatherService.Location = Uri.UnescapeDataString(location);
            return Ok(await _weatherService.GetWeatherAsync());
		}

	}

}
