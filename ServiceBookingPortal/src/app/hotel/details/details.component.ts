import { Component, OnInit } from '@angular/core';
import { HotelService } from '../hotel.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Hotel } from '../hotel';
import { ResponseObject } from 'src/app/response';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  id: number;
  hotel: Hotel;

  constructor(
    public hotelService: HotelService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['hotelId'];
    this.hotelService.find(this.id).subscribe((data: ResponseObject)=>{
      this.hotel = data.payload;
    });
  }

}
