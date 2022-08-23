import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from '../app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { UserService } from './user.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UsersRoutingModule } from './users-routing.module';
import { EditComponent } from './edit/edit.component';
import { IndexComponent } from './index/index.component';
import { AuthInterceptor } from '../authentication.interceptor';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { DetailsComponent } from './details/details.component';
import { CreateComponent } from './create/create.component';



@NgModule({
  declarations: [
    IndexComponent,
    DetailsComponent,
    EditComponent,
    CreateComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    UsersRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    ToastrService,
    UserService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ]
})
export class UsersModule { }
