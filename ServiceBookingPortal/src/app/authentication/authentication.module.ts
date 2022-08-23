import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { AuthInterceptor } from '../authentication.interceptor';
import { RegisterComponent } from './register/register.component';
import { SendOtpComponent } from './send-otp/send-otp.component';
import { ChangePasswordComponent } from './change-password/change-password.component';


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    SendOtpComponent,
    ChangePasswordComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,

    AuthenticationRoutingModule,
  ],
  providers: [
    AuthInterceptor
  ]
})
export class AuthenticationModule { }
