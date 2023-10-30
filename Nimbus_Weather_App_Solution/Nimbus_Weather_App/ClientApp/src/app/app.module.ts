// Import Modules.
import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";

// Import App.
import { AppComponent } from "./app.component";
// Import Components.
import { NavMenuComponent } from "./components/nav-menu/nav-menu.component";
// Import Pages.
import { HomeComponent } from "./pages/home/home.component";
import { LocationSearchComponent } from "./components/location-search/location-search.component";
import { ForecastDisplayComponent } from './components/forecast-display/forecast-display.component';
import { MainHttpInterceptor } from "./services/main-interceptor";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LocationSearchComponent,
    ForecastDisplayComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
    ]),
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MainHttpInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
