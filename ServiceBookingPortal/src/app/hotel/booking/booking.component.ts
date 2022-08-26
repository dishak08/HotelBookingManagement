import { Component, OnInit } from '@angular/core';
import { HotelService } from '../hotel.service';
import { UserService } from 'src/app/users/user.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl, Validators} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Booking } from 'src/app/booking/booking';
import { ResponseObject } from 'src/app/response';
import { Hotel, Sample } from '../hotel';
import { UserResponse } from 'src/app/users/user.model';
import { BookingService } from 'src/app/booking/booking.service';


@Component({
  selector: 'app-booking',
  templateUrl: './booking.component.html',
  styleUrls: ['./booking.component.css']
})
export class BookingComponent implements OnInit {

  id: number;
  form:FormGroup;
  movieName:string;
  userName:string;
  hotel:Hotel;
  userId:number;
  price:number;
  n:number=0;

  constructor(
    public hotelService: HotelService,
    public userService: UserService,
    public bookingService: BookingService,
    private router: Router,
    private route: ActivatedRoute,
    private toastr: ToastrService
  ) { }
 
  ngOnInit(): void {
    this.userId = Number(localStorage.getItem('id'));
    this.userService.search(this.userId).subscribe((data: UserResponse)=>{
      this.userName=data.payload.name;
    });
    this.id = this.route.snapshot.params['hotelId'];
    this.hotelService.find(this.id).subscribe((data: ResponseObject)=>{
      this.hotel = data.payload;
      this.price=this.hotel.price;
    this.form = new FormGroup({
      hotelId: new FormControl(this.hotel.id, Validators.required),
      userId: new FormControl(this.userId, Validators.required),
      noOfRooms: new FormControl('', Validators.required),
      amount: new FormControl('', Validators.required)
    })
  });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    this.form.get('amount')?.setValue(this.price*this.form.get('noOfRooms')?.value);
    this.bookingService.create(this.form.value).subscribe(res => {
      this.toastr.success('Hotel Booking successfully!');
      this.router.navigate(['booking/index']);
    })
  }
}
