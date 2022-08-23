import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {  Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { Booking, Sample } from './booking';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  private apiURL = environment.ConnectedServices.Movie;

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  }

  constructor(private httpClient: HttpClient, private toastr: ToastrService) { }

  getAll(): Observable<any> {

    return this.httpClient.get(this.apiURL + 'api/bookings/')

    .pipe(
      catchError(this.errorHandler)
    )
  }

  create(booking:Sample): Observable<any> {

    return this.httpClient.post(this.apiURL + 'api/bookings/', booking, this.httpOptions)

    .pipe(
      catchError(this.errorHandler)
    )
  }

  find(id: number): Observable<any> {

    return this.httpClient.get(this.apiURL + 'api/bookings/' + id)

    .pipe(
      catchError(this.errorHandler)
    )
  }

  update(id: number, booking:Booking): Observable<any> {

    return this.httpClient.put(this.apiURL + 'api/bookings/' + id, booking, this.httpOptions)

    .pipe(
      catchError(this.errorHandler)
    )
  }

  delete(id:number){
    return this.httpClient.delete(this.apiURL + 'api/bookings/' + id, this.httpOptions)

    .pipe(
      catchError(this.errorHandler)
    )
  }


  errorHandler(error:any) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    this.toastr.error(errorMessage, 'Error');
    return throwError(() => new Error(errorMessage));
 }
}
