
import { HeaderComponent } from './../components/header/header.component';
import { Injectable } from '@angular/core';
import { catchError, tap, finalize } from 'rxjs/operators';
import { HttpClient, HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { localStor } from 'src/app/Services/local-storage.service';
@Injectable({
  providedIn: 'root',
})
export class CommonServiceService{
  public errDetail: any;
  public REST_API_SERVER = environment.apis.default.url;
  constructor(private httpClient: HttpClient,
   ) {}
 
  private httpOptions = {
    headers: new HttpHeaders({
      // 'Content-Type': 'application/json',

      Authorization: `Bearer ${localStor.getToken()}`,
    }),
  };

  /**
   * httpClient
   */
  public returnHttpClient(url) {
    // this.spinnerService.requestStart();
    return this.httpClient.get<any>(url).pipe(catchError(this.handleError));
  }
  public returnHttpClientAuthorize(url) {
    return this.httpClient.get<any>(url, this.httpOptions).pipe(catchError(this.handleError));
  }
  public deleteHttpClient(url) {
    return this.httpClient.delete<any>(url, this.httpOptions).pipe(catchError(this.handleError));
  }
  public postHttpClient(url, data) {
    return this.httpClient
      .post<any>(url, data, this.httpOptions)
      .pipe(catchError(this.handleError));
  }
  public postHttpClientNotAuthorize(url, data) {
    return this.httpClient.post<any>(url, data).pipe(catchError(this.handleError));
  }
  public putHttpClient(url, data) {
    return this.httpClient.put(url, data, this.httpOptions).pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(`Backend returned code ${error.status}, ` + `body was: ${error.error}`);
    }
    // Return an observable with a user-facing error message.
    // this.spinnerService.requestSpinner();
    return throwError(error.error);
  }
}
