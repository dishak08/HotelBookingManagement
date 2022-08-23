import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../user.service';
import { User } from '../user.model';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  form: FormGroup;
  user: User;

  constructor(
    public userService: UserService,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      name: new FormControl("", [Validators.required]),
      email: new FormControl("", [Validators.required]),
      password: new FormControl("", [Validators.required]),
      mobile: new FormControl("", [Validators.required]),
      role: new FormControl(0, [Validators.required]),
    });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    this.form.value.role = parseInt(this.form.value.role);
    this.userService.create(this.form.value).subscribe((res) => {
      this.toastr.success('User created successfully!');
      this.router.navigate(['users']);
    })
  }

}
