import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './auth/user/user.component';
import { RegistrationComponent } from './auth/user/registration/registration.component';
import { LoginComponent } from './auth/user/login/login.component';
import { AuthGuard } from './auth/auth.guard';
import { ForbiddenComponent } from './auth/forbidden/forbidden.component';
import { HomeComponent } from './Reception/home/home.component';
import {GuestsComponent} from './Reception/guests/guests.component';
import {DashboardComponent} from './Management/dashboard/dashboard.component';
import {CalendarComponent} from './Reception/calendar/calendar.component';


const routes: Routes = [
  { path: '', redirectTo: '/user/login', pathMatch: 'full' },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  //{ path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent },
  { path: 'guests', component: GuestsComponent },
  { path: 'forbidden', component: ForbiddenComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'calendar', component: CalendarComponent },


  //{ path: 'adminpanel', component: AdminPanelComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin', 'Manager'] } }

  { path: '**', redirectTo: '/user/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
