import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { AuthorizationRequest } from '../auth.model';
import { AuthService } from '../auth.service';
import { HttpClientModule } from '@angular/common/http';
import { RegisterService } from './register.service';
import { user } from './register.model';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
@Component({
  selector: 'app-login',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form: FormGroup;

  constructor(private userData:RegisterService, private toastr: ToastrService, private router: Router){}

  addUser()
  {
    this.userData.addUser(this.form.value).subscribe((result) => {

      this.router.navigate(['auth/login']);
      this.toastr.success('Registered successfully!');
    })
  }
  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl("", [Validators.required]),
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required]),
      mobile: new FormControl("", [Validators.required]),
      role: new FormControl(0, [Validators.required]),
    });
  }
}
