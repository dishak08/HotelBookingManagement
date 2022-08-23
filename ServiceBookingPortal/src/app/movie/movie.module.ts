import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MovieRoutingModule } from './movie-routing.module';
import { IndexComponent } from './index/index.component';
import { DetailsComponent } from './details/details.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    IndexComponent,
    DetailsComponent,
    CreateComponent,
    EditComponent
  ],
  imports: [
    CommonModule,
    MovieRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class MovieModule { }
