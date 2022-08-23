import { Component } from '@angular/core';
import { AuthService } from './authentication/auth.service';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ServiceBookingPortal';

  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  isAdminUser() {
    return this.authService.isAdmin();
  }

  isLoggedIn() {
    return this.authService.isLoggedIn();
  }

  logout() {
    this.authService.logout();
    this.router.navigate(["/auth/login"]);
  }

  login() {
    this.router.navigate(["/auth/login"]);
  }
}
