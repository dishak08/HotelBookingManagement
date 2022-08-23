import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from '../authentication/auth.service';
import { User, UserResponse } from '../users/user.model';
import { ProfileService } from './profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  userId: number;


  form: FormGroup = new FormGroup({
    id: new FormControl("", [Validators.required]),
    name: new FormControl("", [Validators.required]),
    email: new FormControl("", [Validators.required]),
    mobile: new FormControl("", [Validators.required]),
    role: new FormControl(0, [Validators.required]),
    createdDate: new FormControl("", [Validators.required]),
  });

  user: User;
  disabled: boolean = true;

  constructor(
    private authService: AuthService,
    private profileService: ProfileService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.authService.isLoggedIn() ? null : this.router.navigate(['/auth/login']);
    this.userId = this.authService.getUserId();

    this.profileService.find(this.userId).subscribe((data: UserResponse)=>{
      this.user = data.payload;

      this.form = new FormGroup({
        id: new FormControl({ value: this.user.id, disabled: true }, [Validators.required]),
        name: new FormControl({ value: this.user.name, disabled: this.disabled }, [Validators.required]),
        email: new FormControl({ value: this.user.email, disabled: this.disabled }, [Validators.required]),
        mobile: new FormControl({ value: this.user.mobile, disabled: this.disabled }, [Validators.required]),
        role: new FormControl({ value: this.user.role, disabled: this.disabled }, [Validators.required]),
        createdDate: new FormControl({ value: this.user.registrationDate, disabled: true}, [Validators.required]),
      });
    });
  }

  get f(){
    return this.form.controls;
  }



}
