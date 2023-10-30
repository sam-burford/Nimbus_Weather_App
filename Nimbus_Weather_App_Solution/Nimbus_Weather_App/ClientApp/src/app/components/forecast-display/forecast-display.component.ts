import { Component } from '@angular/core';
import { WeatherApiService } from "../../services/weather-api.service";

@Component({
  selector: 'forecast-display',
  templateUrl: './forecast-display.component.html',
  styleUrls: ['./forecast-display.component.css']
})
export class ForecastDisplayComponent
{

  public forecastData: any;

  constructor(private api: WeatherApiService) { }

  ngOnInit()
  {
    WeatherApiService.weatherUpdates.subscribe((data) =>
    {
      this.forecastData = data;
      console.log(data);
    });

    this.api.getWeather();
  }

}
