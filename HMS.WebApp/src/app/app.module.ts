import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './auth/user/user.component';
import { RegistrationComponent } from './auth/user/registration/registration.component';
import { LoginComponent } from './auth/user/login/login.component';
import { ForbiddenComponent } from './auth/forbidden/forbidden.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { UserService } from './services/user.service';
import { AuthInterceptor } from './auth/auth.interceptor';
import { HomeComponent } from './Reception/home/home.component';
import { GuestsComponent } from './Reception/guests/guests.component';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { DashboardComponent } from './Management/dashboard/dashboard.component';
import { CalendarComponent } from './Reception/calendar/calendar.component';
import { RoomsComponent } from './Reception/rooms/rooms.component';
import { GroupsComponent } from './Reception/groups/groups.component';
import { ContactUsComponent } from './Settings/contact-us/contact-us.component';
import { AngularMaterialModule } from './modal/angular-material.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CheckReservationModalComponent } from './Reception/home/check-reservation-modal/check-reservation-modal.component';
import { NewReservationModalComponent } from './Reception/home/new-reservation-modal/new-reservation-modal.component';
import { GroupReservationModalComponent } from './Reception/home/group-reservation-modal/group-reservation-modal.component';
import { ArchwizardModule } from 'angular-archwizard';
import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { AddGroupModalComponent } from './Reception/groups/add-group-modal/add-group-modal.component';
import { CompaniesComponent } from './Management/companies/companies.component';
import { AddCompanyComponent } from './Management/companies/add-company/add-company.component';
import { AddCompanyModalComponent } from './Reception/groups/add-company-modal/add-company-modal.component';


export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    RegistrationComponent,
    LoginComponent,
    ForbiddenComponent,
    HomeComponent,
    GuestsComponent,
    SidebarComponent,
    DashboardComponent,
    CalendarComponent,
    RoomsComponent,
    GroupsComponent,
    ContactUsComponent,
    CheckReservationModalComponent,
    NewReservationModalComponent,
    GroupReservationModalComponent,
    ConfirmDialogComponent,
    AddGroupModalComponent,
    CompaniesComponent,
    AddCompanyModalComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AngularMaterialModule,
    ArchwizardModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader, // exported factory function needed for AoT compilation
        deps: [HttpClient]
      }
    }),
    ToastrModule.forRoot({
      progressBar: true
    }),
    FormsModule
  ],
  providers: [UserService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  entryComponents: [CheckReservationModalComponent, NewReservationModalComponent, GroupReservationModalComponent,
    ConfirmDialogComponent, AddGroupModalComponent, AddCompanyModalComponent]
})
export class AppModule { }
