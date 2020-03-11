import { Component, OnInit, Inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { Reservation } from 'src/app/models/Reservation';
import { ReservationService } from 'src/app/services/Reservation.service';
import { Person } from 'src/app/models/Person';
import { LoginComponent } from '../../../auth/user/login/login.component';
import { PersonService } from '../../../services/person.service';
import { AddPersonModalComponent } from '../../persons/add-person-modal/add-person-modal.component';

@Component({
  selector: 'app-new-reservation-modal',
  templateUrl: './new-reservation-modal.component.html',
  styleUrls: ['./new-reservation-modal.component.css']
})
export class NewReservationModalComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    public reservationService: ReservationService,
    public personService: PersonService,
    public dialogRef: MatDialogRef<NewReservationModalComponent>, public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data) { }

  close(): void {
    this.dialogRef.close();
  }

  reservation = new Reservation();
  selectedPersons: any;
  TotalCost: number;
  BaseTotalCost: number;
  enableNextStep: boolean;
  availableRooms: any;
  personsList: Person[];

  ngOnInit() {
    this.selectedPersons = new Array();
    this.personService.getAll().subscribe((response: Person[]) => {
      this.personsList = response;
    });

    if (this.data !== null) {
      this.reservation.StartDate = this.data.StartDate;
      this.reservation.EndDate = this.data.EndDate;
    }
  }

  AddPerson() {
    this.dialog.open(AddPersonModalComponent, {
      width: '350px'
    }).afterClosed().subscribe(result => {
      if (typeof result !== 'undefined')
        this.personsList.push(result);
    });
  }

  checkAvailableRooms() {
    if (this.reservation.StartDate === this.reservation.EndDate) {
      this.toastr.error('Checkout must be bigger than Checkin')
      return;
    }

    this.reservationService.Check(this.reservation).subscribe(response => {
      this.availableRooms = response;
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }

  SaveReservation() {
    this.reservation.HotelId = "3AB92D5C-33D1-4D17-83F3-A1CC5E00C4CD";
    this.reservation.code = "RSV1098";
    this.reservation.UserId = "1DC97D96-BB38-4D7C-BCA3-111BADE204CB";
    this.reservation.TotalCost = this.TotalCost;


    this.reservationService.Save(this.reservation, this.selectedPersons).subscribe(response => {
      this.dialogRef.close(response);
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }

  OnSelectPerson(selectedPerson) {
    var room = this.availableRooms.find(e => e.id == this.reservation.RoomId);
    if (this.selectedPersons.length < room.totalBeds) {
      var person = Object.assign({}, selectedPerson);
      person.RoomId = room.id;
      this.selectedPersons.push(person);
      this.personsList.splice(this.personsList.indexOf(selectedPerson), 1);
    }
  }

  Next() {
    var cost = this.availableRooms.find(e => e.id == this.reservation.RoomId).cost;
    var days = this.date_diff_indays(this.reservation.StartDate, this.reservation.EndDate);
    this.TotalCost = days * cost;
    this.BaseTotalCost = this.TotalCost;
  }

  date_diff_indays(date1, date2) {
    var dt1 = new Date(date1);
    var dt2 = new Date(date2);
    return Math.floor((Date.UTC(dt2.getFullYear(), dt2.getMonth(), dt2.getDate()) - Date.UTC(dt1.getFullYear(), dt1.getMonth(), dt1.getDate())) / (1000 * 60 * 60 * 24));
  }

  resetRooms() {
    this.availableRooms = [];
    console.log("Reset rooms");

  }

  Discount(value) {
    var discount = (this.BaseTotalCost * value) / 100;
    this.TotalCost = this.BaseTotalCost - discount;
  }
}
