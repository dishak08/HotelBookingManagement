import { Component, OnInit } from '@angular/core';
import { BookingService } from '../booking.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Booking } from '../booking';
import { ResponseObject } from 'src/app/response';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  id: number;
  booking: Booking;

  constructor(
    public bookingService: BookingService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['bookingId'];
    this.bookingService.find(this.id).subscribe((data: ResponseObject)=>{
      this.booking = data.payload;
    });
  }

}
