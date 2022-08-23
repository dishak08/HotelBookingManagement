import { Component, OnInit } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';

import { UserService } from '../user.service';
import { User, UserResponse } from '../user.model';
import { Role } from 'src/app/authentication/auth.model';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  id: number;
  user: User;

  constructor(
    public userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
    this.userService.find(this.id).subscribe((data: UserResponse)=>{
      this.user = data.payload;
    });
  }

}
