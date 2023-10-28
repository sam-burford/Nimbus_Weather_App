using Nimbus_Weather_App.Models;

namespace Nimbus_Weather_App.Interfaces
{

	public interface IWeatherService
	{

		public Task<WeatherResponse> GetWeatherAsync();

	}

}
