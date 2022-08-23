import { Component, OnInit } from '@angular/core';
import { BookingService } from '../booking.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import {Booking, Sample} from '../booking';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {

  form:FormGroup;
  booking:Sample;

  constructor(
    public bookingService: BookingService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.form = new FormGroup({
      movieId: new FormControl('', [Validators.required]),
      userId: new FormControl('', Validators.required),
      noOfSeats: new FormControl('', Validators.required),
      totalCost: new FormControl('', Validators.required)
    });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    this.bookingService.create(this.form.value).subscribe(res => {
      this.toastr.success('Booking created successfully!');
      this.router.navigate(['booking/index']);
    })
  }

}
