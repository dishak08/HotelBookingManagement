import { HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable } from "rxjs";
import { tap } from "rxjs/operators";
import { AuthService } from "./authentication/auth.service";


@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private toastr: ToastrService, private router: Router, private authService: AuthService) { }

  intercept(req: HttpRequest<any>,
            next: HttpHandler): Observable<HttpEvent<any>> {

      const idToken = localStorage.getItem("access_token");
      if (idToken) {
          const cloned = req.clone({
              headers: new HttpHeaders({
                  "Authorization": "Bearer " + idToken
                })
          });

          return next.handle(cloned).pipe(
            tap((event: HttpEvent<any>) => {
            }),
            tap((err: any) => {
              if (err.status === 401) {
                this.toastr.error("Authorization Failed")
                this.authService.logout();
                this.router.navigate(["/auth/login"])
              }
            }));
      }
      else {
          return next.handle(req).pipe(
            tap((event: HttpEvent<any>) => {
            }),
            tap((err: any) => {
              if (err.status === 401) {
                this.toastr.error("Authorization Failed")
                this.authService.logout();
                this.router.navigate(["/auth/login"])
              }
            }));
      }
  }
}
