import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { user } from './register.model';
import { environment } from 'src/environments/environment';
import { catchError, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  apiUrl = environment.ConnectedServices.User;

  constructor(private http:HttpClient, private toastr: ToastrService) { }

  addUser(data: user)
  {
    return this.http.post(this.apiUrl + "user/", data).pipe(
      catchError(this.errorHandler.bind(this))
    )
  }

  errorHandler(error:any) {
    let errorMessage = '';
    if(error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    }
    else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }

    this.toastr.error(errorMessage, 'Error');
    return throwError(() => new Error(errorMessage));
  }
}
