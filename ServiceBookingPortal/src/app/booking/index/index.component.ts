import { Component, OnInit } from '@angular/core';
import { BookingService } from '../booking.service';
import { Booking } from '../booking';
import { ResponseObject } from 'src/app/response';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  bookings: Booking[];
  authService: any;

  constructor(public bookingService: BookingService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.bookingService.getAll().subscribe((data: ResponseObject)=>{
      this.bookings = data.payload;
      console.log(this.bookings);
    })
  }

  deleteBooking(id:number){
    this.bookingService.delete(id).subscribe(res => {
         this.bookings = this.bookings.filter(item => item.id !== id);
         this.toastr.success('Movie deleted successfully!');
    })
  }

  isAdminUser() {
    return this.authService.isAdmin();
  }

}
