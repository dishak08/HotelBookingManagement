import { Component, OnInit } from '@angular/core';
import { MovieService } from '../movie.service';
import { Movie } from '../movie';
import { ResponseObject } from 'src/app/response';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  movies: Movie[];
  authService: any;

  constructor(public movieService: MovieService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.movieService.getAll().subscribe((data: ResponseObject)=>{
      this.movies = data.payload;
      console.log(this.movies);
    })
  }

  deleteMovie(id:number){
    this.movieService.delete(id).subscribe(res => {
         this.movies = this.movies.filter(item => item.id !== id);
         this.toastr.success('Movie deleted successfully!');
    })
  }

  isAdminUser() {
    return this.authService.isAdmin();
  }

}
