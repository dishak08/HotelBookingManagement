import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookingComponent } from './booking/booking.component';
import { CreateComponent } from './create/create.component';
import { DetailsComponent } from './details/details.component';
import { EditComponent } from './edit/edit.component';
import { IndexComponent } from './index/index.component';

const routes: Routes = [
  { path: 'hotel', redirectTo: 'hotel/index', pathMatch: 'full'},
  { path: 'hotel/index', component: IndexComponent },
  { path: 'hotel/details/:hotelId', component: DetailsComponent },
  { path: 'hotel/create', component: CreateComponent },
  { path: 'hotel/edit/:hotelId', component: EditComponent },
  { path: 'hotel/booking/:hotelId', component: BookingComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HotelRoutingModule { }
