import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Observable, throwError, catchError } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User, UserResponse } from '../users/user.model';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  apiUrl = environment.ConnectedServices.User;

  constructor(private httpClient: HttpClient, private router: Router, private toastr: ToastrService) { }


  find(id: number): Observable<UserResponse> {
    return this.httpClient.get(this.apiUrl + "user/" + id).pipe(
      catchError<any, any>(this.errorHandler.bind(this))
    );
  }

  update(id: number, user: User): Observable<UserResponse> {
    return this.httpClient.put(this.apiUrl + "user/" + id, user).pipe(
      catchError<any, any>(this.errorHandler.bind(this))
    );
  }

  errorHandler(error:any) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    this.router.navigate(['/auth/login']);

    this.toastr.error(errorMessage, 'Error');
    return throwError(() => new Error(errorMessage));
  }
}
