using Microsoft.AspNetCore.Mvc;
using Nimbus_Weather_App.Services;

namespace Nimbus_Weather_App.Controllers
{

    [Route("api/[controller]")]
	[ApiController]
	public class WeatherController : ControllerBase
	{

		private readonly WeatherService _weatherService;

		public WeatherController(WeatherService service)
		{
			_weatherService = service;
			service.Location = "London";
		}

        [HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _weatherService.GetWeatherAsync());
		}

	}

}
