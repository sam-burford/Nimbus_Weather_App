import { Injectable } from '@angular/core';
import
{
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from "rxjs";

@Injectable()
export class MainHttpInterceptor implements HttpInterceptor
{
  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>>
  {
    return next.handle(request).pipe(
      catchError((error: any) =>
      {
        if (error instanceof HttpErrorResponse)
        {
          // Handle HTTP errors (e.g., status code, error message)
          if (error.status !== 0)
          {
            console.error('HTTP Error:', error.status, error.message);
          }
        } else
        {
          // Handle other errors (e.g., network errors)
          console.error('Network Error:', error);
        }

        return throwError(error); // Re-throw the error to propagate it to subscribers
      })
    );
  }
}
