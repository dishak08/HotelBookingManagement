import { Component, OnInit } from '@angular/core';
import { MovieService } from '../movie.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from '../movie';
import { ResponseObject } from 'src/app/response';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  id: number;
  movie: Movie;

  constructor(
    public movieService: MovieService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['movieId'];
    this.movieService.find(this.id).subscribe((data: ResponseObject)=>{
      this.movie = data.payload;
    });
  }

}
