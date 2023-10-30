using Nimbus_Weather_App.Models;

namespace Nimbus_Weather_App.Interfaces
{

	public interface IWeatherService
	{

		/// <summary>
		/// Retrieves weather data from an external API and stores it in a WeatherResponse variable. 
		/// </summary>
		/// <returns></returns>
		public Task<WeatherResponse> GetWeatherAsync();

		/// <summary>
		/// The HttpClient used for making calls to an external API. 
		/// </summary>
		public HttpClient Http { get; }
		/// <summary>
		/// The current location of the weather object. 
		/// </summary>
		public string Location { get; set; }
		/// <summary>
		/// This is the value of the Weather Reponse most recently cached, 
		/// or null if it has not yet been cached. 
		/// </summary>
		public WeatherResponse? CachedWeatherReponse { get; }
		/// <summary>
		/// This is the DateTime when the Weather Response was most recently cached, 
		/// or DateTime.Min if it has not yet been cached. 
		/// </summary>
		public DateTime CachedWeatherReponseTime { get; }

	}

}
