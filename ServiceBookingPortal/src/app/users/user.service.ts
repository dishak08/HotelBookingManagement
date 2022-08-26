import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AuthService } from '../authentication/auth.service';
import { User, UserCreate, UserListResponse, UserResponse } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  apiUrl: string = environment.ConnectedServices.User;

  constructor(private httpClient: HttpClient, private toastr: ToastrService, private router: Router) { }

  getAll(): Observable<UserListResponse> {
    return this.httpClient.get(this.apiUrl + "user").pipe(
      catchError<any, any>(this.errorHandler.bind(this))
    );
  }

  create(user: UserCreate): Observable<UserResponse> {
    return this.httpClient.post(this.apiUrl + "user", user).pipe(
      catchError<any, any>(this.errorHandler.bind(this))
    );
  }

  search(id: number): Observable<UserResponse> {
    return this.httpClient.get(this.apiUrl + "user/" + id).pipe(
      catchError<any, any>(this.errorHandler.bind(this))
    );
  }

  update(id: number, user: User): Observable<UserResponse> {
    return this.httpClient.put(this.apiUrl + "user/" + id, user).pipe(
      catchError<any, any>(this.errorHandler.bind(this))
    );
  }

  delete(id: number): Observable<Response> {
    return this.httpClient.delete(this.apiUrl + "user/" + id).pipe(
      catchError<any, any>(this.errorHandler.bind(this))
    );
  }

  getMovie(id: number): Observable<any>{
    return this.httpClient.get("https://localhost:44344/api/movies/" + id)

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

    this.router.navigate(['/auth/login']);

    this.toastr.error(errorMessage, 'Error');
    return throwError(() => new Error(errorMessage));
  }
}
