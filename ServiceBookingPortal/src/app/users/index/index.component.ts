import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { Movie, MovieResponse, User, UserListResponse } from '../user.model';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {

  users: User[];
  movie:Movie
  name:string

  constructor(public userService: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userService.getAll().subscribe((data: UserListResponse)=>{
      this.users = data.payload;})

      this.userService.getMovie(2).subscribe((data:MovieResponse)=>{
        this.movie=data.payload;
      })
  }

  deletePost(id:number){
    this.userService.delete(id).subscribe(res => {
         this.users = this.users.filter(item => item.id !== id);
         this.toastr.info('User deleted successfully!');
    })
  }

}
