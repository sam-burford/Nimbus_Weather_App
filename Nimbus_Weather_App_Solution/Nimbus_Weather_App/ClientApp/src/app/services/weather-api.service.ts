import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Subject, catchError, of } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class WeatherApiService
{

  protected static _weatherUpdates = new Subject<any>();
  /**This is a Observable which receives posts whenever the weather data is updated. */
  public static weatherUpdates = WeatherApiService._weatherUpdates.asObservable();

  /** Stores the location for the Weather API. */
  protected static location: string = "London";

  constructor(private http: HttpClient) { }

  /**
   * Gets the stored location last used by the Weather API Service. 
   * @returns
   */
  getLocation(): string
  {
    return WeatherApiService.location;
  }

  /**
   * Updates the location for the Weather API Service. 
   * @param location The new location. 
   */
  setLocation(location: string): void
  {
    WeatherApiService.location = location;
  }

  /**
   * Retrieves a WeatherResponse from the back-end API via a GET request.
   * @author Sam Burford
   */
  getWeather(): any
  {
    const encodedLocation = encodeURI(this.getLocation());
    const req = this.http.get<any>("https://localhost:7185/api/weather/" + encodedLocation).pipe(catchError(error =>
    {
      console.error("Error while retrieving weather forecast: ", error);
      return of(0);
    }));

    req.subscribe((res) =>
    {
      WeatherApiService._weatherUpdates.next(res);
    });

    return req;
  }

}
