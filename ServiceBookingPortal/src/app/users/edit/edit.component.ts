import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { ResponseObject } from 'src/app/response';
import { UserService } from '../user.service';
import { User, UserResponse } from '../user.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id: number;
  user: User;
  form: FormGroup = new FormGroup({
    id: new FormControl("", [Validators.required]),
    name: new FormControl("", [Validators.required]),
    email: new FormControl("", [Validators.required]),
    mobile: new FormControl("", [Validators.required]),
    role: new FormControl("", [Validators.required]),
    createdDate: new FormControl("", [Validators.required]),
  });

  constructor(
    public userService: UserService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['userId'];
    console.log(this.id)
    this.userService.find(this.id).subscribe((data: UserResponse)=>{
      this.user = data.payload;

      this.form = new FormGroup({
        id: new FormControl(this.user.id, [Validators.required]),
        name: new FormControl(this.user.name, [Validators.required]),
        email: new FormControl(this.user.email, [Validators.required]),
        mobile: new FormControl(this.user.mobile, [Validators.required]),
        role: new FormControl({ value: this.user.role, disabled: true }, [Validators.required]),
        createdDate: new FormControl({ value: '' , disabled: true}, [Validators.required]),
      });
    });


  }

  get f(){
    return this.form.controls;
  }

  submit(){
    console.log(this.form.value);
    this.userService.update(this.id, this.form.value).subscribe(res => {
         this.router.navigate(['users']);
         this.toastr.success('User updated successfully!');
    })
  }

}
