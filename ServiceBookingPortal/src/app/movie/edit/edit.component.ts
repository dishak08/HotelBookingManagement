import { Component, OnInit } from '@angular/core';
import { MovieService } from '../movie.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Movie } from '../movie';
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
  movie: Movie;
  form: FormGroup;

  constructor(
    public movieService: MovieService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['movieId'];
    this.movieService.find(this.id).subscribe((data: ResponseObject)=>{
      this.movie = data.payload;
    });

    this.form = new FormGroup({
      id: new FormControl('', [Validators.required]),
      name: new FormControl('', [Validators.required]),
      price: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      type: new FormControl('', Validators.required)
    });
  }

  get f(){
    return this.form.controls;
  }

  submit(){
    console.log(this.form.value);
    this.movieService.update(this.id, this.form.value).subscribe(res => {
      this.router.navigateByUrl('movie/index');
      this.toastr.success('Movie updated successfully!');
    })
  }

}
