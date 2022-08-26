import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { IndexComponent } from './index/index.component';

const routes: Routes = [
  { path: 'booking', redirectTo: 'booking/index', pathMatch: 'full'},
  { path: 'booking/index', component: IndexComponent },
  { path: 'booking/details/:bookingId', component: DetailsComponent },
  { path: 'booking/create', component: CreateComponent },
  { path: 'booking/edit/:bookingId', component: EditComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BookingRoutingModule { }
