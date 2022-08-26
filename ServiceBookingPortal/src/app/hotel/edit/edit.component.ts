import { Component, OnInit } from '@angular/core';
import { HotelService } from '../hotel.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Hotel } from '../hotel';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { ResponseObject } from 'src/app/response';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  id: number;
  hotel: Hotel;
  form: FormGroup;

  constructor(
    public hotelService: HotelService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['hotelId'];
    this.hotelService.find(this.id).subscribe((data: ResponseObject)=>{
      this.hotel = data.payload;
    });

    this.form = new FormGroup({
      id: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      price: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      city: new FormControl('', Validators.required),
      phoneNo: new FormControl('', Validators.required)
    });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    console.log(this.form.value);
    this.hotelService.update(this.id, this.form.value).subscribe(res => {
      this.router.navigateByUrl('hotel/index');
      this.toastr.success('Hotel updated successfully!');
    })
  }

}
