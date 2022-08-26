import { Component, OnInit } from '@angular/core';
import { HotelService } from '../hotel.service';
import { Hotel } from '../hotel';
import { ResponseObject } from 'src/app/response';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/authentication/auth.service';


@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  hotels: Hotel[];

  constructor(public hotelService: HotelService,public authService: AuthService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.hotelService.getAll().subscribe((data: ResponseObject)=>{
      this.hotels = data.payload;
      console.log(this.hotels);
    })
  }

  deleteMovie(id:number){
    this.hotelService.delete(id).subscribe(res => {
         this.hotels = this.hotels.filter(item => item.id !== id);
         this.toastr.success('Hotel deleted successfully!');
    })
  }

  isAdminUser() {
    return this.authService.isAdmin();
  }

}
