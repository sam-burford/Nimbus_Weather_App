using System.Text.Json.Serialization;

namespace Nimbus_Weather_App.Models
{

	public class WeatherResponse
	{

		public WeatherResponse()
		{
			IsValid = true;
		}

        public LocationInfo? Location { get; set; }
        public CurrentInfo? Current { get; set; }
		public bool IsValid { get; private set; }

		/// <summary>
		/// Represents an invalid Weather Response. For example, this is the result of a failed parsing. 
		/// </summary>
		/// <returns></returns>
		public static WeatherResponse Invalid()
		{
			return new WeatherResponse()
			{
				IsValid = false
			};
		}

    }

	public class LocationInfo
	{

		public string Name { get; set; } = "";
		public string Region { get; set; } = "";
		public string Country { get; set; } = "";
		public double Lat { get; set; }
		public double Lon { get; set; }

	}

	public class CurrentInfo
	{

		public string? LastUpdated { get; set; }
		[JsonPropertyName("temp_c")]
		public double TemperatureCelsius { get; set; }
		[JsonPropertyName("temp_f")]
		public double TemperatureFahrenheit { get; set; }
		[JsonPropertyName("is_day")]
		public int IsDay { get; set; }
        public Condition Condition { get; set; }
        [JsonPropertyName("wind_mph")]
        public double WindMph { get; set; }
        [JsonPropertyName("wind_kph")]
        public double WindKph { get; set; }
        [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }
        [JsonPropertyName("wind_dir")]
        public string? WindDir { get; set; }
        public int Humidity { get; set; }
        public int Cloud { get; set; }

	}

	public struct Condition
	{
        public Condition()
        {

        }

        public string Text { get; set; } = "";
		public string Icon { get; set; } = "";
		public int Code { get; set; } = 0;

    }

}
