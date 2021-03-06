import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { ReservationService } from 'src/app/services/Reservation.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialog } from '@angular/material';
import { CheckReservationModalComponent } from './check-reservation-modal/check-reservation-modal.component';
import { NewReservationModalComponent } from './new-reservation-modal/new-reservation-modal.component';
import { GroupReservationModalComponent } from './group-reservation-modal/group-reservation-modal.component';
import { Reservation } from '../../models/Reservation';
import { ConfirmDialogComponent } from '../../components/confirm-dialog/confirm-dialog.component';
import { Observable, observable } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private router: Router, translate: TranslateService, private reservationService: ReservationService,
    private toastr: ToastrService, public dialog: MatDialog) {
    translate.setDefaultLang('en');
    // the lang to use, if the lang isn't available, it will use the current loader to get them
    translate.use('en');
  }

  ngOnInit() {
    this.reservationService.Get().subscribe((response: Reservation[]) => {
      this.reservations = response;
    }
      , error => {
        this.toastr.error(error.message);
        console.log(error);
      });
  }

  reservations: Reservation[];
  selectedReservation: Reservation;
  activeState: string;

  onLogout() {
    localStorage.removeItem('token');
    this.router.navigate(['user/login']);
  }

  BookingCheck(): void {
    this.dialog.open(CheckReservationModalComponent, {
      width: '800px'
    });
  }

  IndividualReservationModal(): void {
    this.dialog.open(NewReservationModalComponent, {
      width: '800px'
    }).afterClosed().subscribe(result => {
      if (typeof result !== 'undefined')
        this.reservations.push(result);
    });
  }

  GroupReservationModal(): void {
    this.dialog.open(GroupReservationModalComponent, {
      width: '1000px'
    }).afterClosed().subscribe(result => {
      if (typeof result !== 'undefined')
        this.reservations.push(result);
    });
  }

  deleteItem(reservation: Reservation) {
    this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: "Are you sure you want to delete " + reservation.code + " ?"
    }).afterClosed().subscribe(result => {
      if (result) {
        this.reservationService.Delete(reservation.id).subscribe(() => {
          var index = this.reservations.indexOf(reservation, 0);
          if (index > -1) {
            this.reservations.splice(index, 1);
          }
        }, error => {
          this.toastr.error(error.error.message);
          this.toastr.error(error.message);
          console.log(error);
        });
      }
    });
  }

  setSelectedReservation(reservation: Reservation) {
    this.selectedReservation = reservation;
    this.activeState = reservation.id;
  }

  CheckIn() {
    if (typeof this.selectedReservation === 'undefined')
      return;

    this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: "Are you sure you want to Check in " + this.selectedReservation.code + " ?"
    }).afterClosed().subscribe(result => {
      if (result) {
        this.reservationService.CheckIn(this.selectedReservation.id).subscribe((result: Boolean) => {
          if (result) {
            this.selectedReservation.statusId = 1;
            this.selectedReservation.status.enName = "Arrived";
          }
        }, error => {
          this.toastr.error(error.error.message);
          this.toastr.error(error.message);
          console.log(error);
        });
      }
    });
  }

  CheckOut() {
    if (typeof this.selectedReservation === 'undefined')
      return;

    this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: "Are you sure you want to Check out " + this.selectedReservation.code + " ?"
    }).afterClosed().subscribe(result => {
      if (result) {
        this.reservationService.CheckOut(this.selectedReservation.id)
          .subscribe((result: Boolean) => {
            if (result) {
              this.selectedReservation.statusId = 3;
              this.selectedReservation.status.enName = "Checked Out";
            }
          }, error => {
            this.toastr.error(error.error.message);
            this.toastr.error(error.message);
            console.log(error);
          });
      }
    });
  }
}
