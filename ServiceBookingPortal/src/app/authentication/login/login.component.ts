import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthorizationRequest } from '../auth.model';
import { AuthService } from '../auth.service';

interface LoginRequestForm {
  email: FormControl<string | null>,
  password: FormControl<string | null>
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private authService: AuthService, private router: Router) { }

  public loginRequestForm: FormGroup<LoginRequestForm> = new FormGroup<LoginRequestForm>({
    email: new FormControl<string>(''),
    password: new FormControl<string>('')
  });


  ngOnInit(): void {
  }


  onLoginSubmit() {
    const request: AuthorizationRequest = {
      email: this.loginRequestForm.value.email ?? "",
      password: this.loginRequestForm.value.password ?? ""
    }
    this.authService.login(request);
    this.router.navigate(["/"]);
  }
}
