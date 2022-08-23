import { Component, OnInit } from '@angular/core';
import { BookingService } from '../booking.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Booking } from '../booking';
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
  booking: Booking;
  form: FormGroup;

  constructor(
    public bookingService: BookingService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['bookingId'];
    this.bookingService.find(this.id).subscribe((data: ResponseObject)=>{
      this.booking = data.payload;
    });

    this.form = new FormGroup({
      id: new FormControl('', [Validators.required]),
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
    console.log(this.form.value);
    this.bookingService.update(this.id, this.form.value).subscribe(res => {
      this.router.navigateByUrl('booking/index');
      this.toastr.success('Booking updated successfully!');
    })
  }

}
