using System.Text.Encodings.Web;
using System.Text.Json;
using Nimbus_Weather_App.Models;

namespace Nimbus_Weather_App.Services
{

    public class WeatherService
    {

        /// <summary>
        /// The base URL for the weather API. 
        /// </summary>
        private const string API_URL = "https://api.weatherapi.com/v1";
        /// <summary>
        /// Secret API key for my account. 
        /// </summary>
        private const string API_KEY = "bb85f0067ec546a38d1160331232610";
        /// <summary>
        /// This is the amount of time the Weather Reponse will be cached for, 
        /// before needing to be retrieved again. 
        /// (In Minutes)
        /// </summary>
        private const int MAX_CACHE_TIME = 5;

        private WeatherResponse? cachedWeatherResponse;
        private DateTime cachedWeatherReponseTime = DateTime.MinValue;

        public WeatherService(HttpClient http)
        {
            Http = http;
            Location = "London";
        }

        /// <summary>
        /// The HttpClient used for making calls to an external API. 
        /// </summary>
        public HttpClient Http { get; private set; }
        /// <summary>
        /// The current location of the weather object. 
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// This is the value of the Weather Reponse most recently cached, 
        /// or null if it has not yet been cached. 
        /// </summary>
        public WeatherResponse? CachedWeatherReponse { get => cachedWeatherResponse; }
        /// <summary>
        /// This is the DateTime when the Weather Response was most recently cached, 
        /// or DateTime.Min if it has not yet been cached. 
        /// </summary>
        public DateTime CachedWeatherReponseTime { get => cachedWeatherReponseTime; }

        /// <summary>
        /// Retrieves weather data from an external API and stores it in a WeatherResponse variable. 
        /// </summary>
        /// <returns></returns>
        public async Task<WeatherResponse> GetWeatherAsync()
        {
            if (Location is null || string.IsNullOrEmpty(Location))
            {
                Console.WriteLine("Location has not been set!");
                return WeatherResponse.Invalid();
            }

            // Check if response has been cached. 
            if (cachedWeatherResponse is not null)
            {
                DateTime currentTime = DateTime.Now;
                TimeSpan elapsedTime = currentTime - cachedWeatherReponseTime;

                // Check when the response was last retrieved. 
                if (elapsedTime < TimeSpan.FromMinutes(MAX_CACHE_TIME))
                {
                    return cachedWeatherResponse;
                }
            }

            string url = API_URL + $"/current.json?key={API_KEY}&q={Location}&aqi=no";
            string json = await Http.GetStringAsync(url);

            if (TryParseWeatherResponse(json, out var weatherReponse))
            {
                // Update cached response. 
                cachedWeatherResponse = weatherReponse;
                cachedWeatherReponseTime = DateTime.Now;

                return weatherReponse;
            }

            return WeatherResponse.Invalid();
        }

        /// <summary>
        /// Retrieves the current temperature of the stored location in degrees celcius. 
        /// </summary>
        /// <returns>String object in the format: '20 °C' or an empty String if not found. </returns>
        public async Task<string> GetCurrentTemperatureCelcius()
        {
            if (Location is null)
            {
                Console.WriteLine("Location has not been set!");
                return "";
            }

            string url = API_URL + $"/current.json?key={API_KEY}&q={GetEncodedLocation()}&aqi=no";
            string json = await Http.GetStringAsync(url);

            if (TryParseWeatherResponse(json, out var weatherReponse))
            {
                return weatherReponse.Current?.TemperatureCelsius.ToString() + " °C";
            }

            return "";
        }

        /// <summary>
        /// Deserialises a JSON response into a weather response. 
        /// </summary>
        /// <param name="jsonResponse">The JSON response. </param>
        /// <param name="weatherResponse">The parsed Weather Response object. </param>
        /// <returns>Whether or not the parsing was successful. </returns>
        private bool TryParseWeatherResponse(string jsonResponse, out WeatherResponse weatherResponse)
        {
            WeatherResponse? result;

            try
            {
                result = JsonSerializer.Deserialize<WeatherResponse>(jsonResponse, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to parse Weather Response: {ex}");
                result = null;
            }

            if (result is null)
            {
                weatherResponse = WeatherResponse.Invalid();
                return false;
            }

            weatherResponse = result;
            return true;
        }

        /// <summary>
        /// Checks if the current stored location is a valid location. 
        /// </summary>
        /// <returns>True or false, representing whether or not it is valid. </returns>
        public bool IsLocationValid()
        {
            return !string.IsNullOrEmpty(Location.Trim());
        }

        /// <summary>
        /// Gets the stored location as a URL-friendly string. 
        /// </summary>
        public string GetEncodedLocation()
        {
            return GetEncodedLocation(Location);
        }

        /// <summary>
        /// Gets the specified location as a URL-friendly string. 
        /// </summary>
        public static string GetEncodedLocation(string location)
        {
            return location is not null ? UrlEncoder.Default.Encode(location) : "";
        }

    }

}
