import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './index/index.component';
import { DetailsComponent } from './details/details.component';
import { CreateComponent } from './create/create.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  { path: 'movie', redirectTo: 'movie/index', pathMatch: 'full'},
  { path: 'movie/index', component: IndexComponent },
  { path: 'movie/details/:movieId', component: DetailsComponent },
  { path: 'movie/create', component: CreateComponent },
  { path: 'movie/edit/:movieId', component: EditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MovieRoutingModule { }
