import { Component } from "@angular/core";
import { WeatherApiService } from "../../services/weather-api.service";

@Component({
  selector: "location-search",
  templateUrl: "./location-search.component.html",
  styleUrls: ["./location-search.component.css"],
})
export class LocationSearchComponent
{

  locationText: string = "";

  constructor(private api: WeatherApiService) { }

  search(): void
  {
    if (this.locationText.trim() == "")
      return;

    this.api.setLocation(this.locationText);
    this.api.getWeather();
  }

}
