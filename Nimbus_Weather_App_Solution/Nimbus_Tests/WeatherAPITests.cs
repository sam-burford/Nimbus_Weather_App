using Nimbus_Weather_App.Models;
using Nimbus_Weather_App.Services;
using Xunit.Abstractions;

namespace Nimbus_Tests
{
    public class WeatherAPITests : IDisposable
	{

		private readonly ITestOutputHelper output;

		public WeatherAPITests(ITestOutputHelper output)
		{
			Weather = new WeatherService(new HttpClient());

			// Assign test output. 
			this.output = output;
		}

        private WeatherService Weather { get; set; }

        public void Dispose()
		{
			
		}

		[Fact]
		public void SetLocation()
		{
			// Arrange
			Weather.Location = "London";

			// Assert
			Assert.Equal("London", Weather.Location);
		}

		[Theory]
		[InlineData("London", true)]
		[InlineData("", false)]
		[InlineData("GU15 3EY", true)]
		[InlineData("Camberley, Surrey, UK", true)]
		public async void LocationValid(string location, bool expected)
		{
			// Arrange
			Weather.Location = location;

			// Act
			bool isValid = Weather.IsLocationValid();
			WeatherResponse result = await Weather.GetWeatherAsync();

			output.WriteLine($"Location Variable: {Weather.Location}");
			output.WriteLine($"Encoded Location: {Weather.GetEncodedLocation()}");

			// Assert
			Assert.Equal(expected, isValid);
			Assert.Equal(expected, result.IsValid);
		}

		[Fact]
		public async void WeatherReponseIsCached()
		{
			// Arrange
			Weather.Location = "London";

			// Act
			await Weather.GetWeatherAsync();

			// Assert
			Assert.NotNull(Weather.CachedWeatherReponse);
		}

		[Fact]
		public async void CurrentTemperatureFormatValid()
		{
			// Arrange
			Weather.Location = "London";

			// Act
			var temp = await Weather.GetCurrentTemperatureCelcius();

			// Assert
			Assert.Contains(" °C", temp);
		}

	}
}