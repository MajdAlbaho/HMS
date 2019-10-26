import { Component, OnInit, Inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Reservation } from 'src/app/models/Reservation';
import { HomeService } from 'src/app/services/home.service';
import { Person } from 'src/app/models/Person';
import { LoginComponent } from '../../../auth/user/login/login.component';

@Component({
  selector: 'app-new-reservation-modal',
  templateUrl: './new-reservation-modal.component.html',
  styleUrls: ['./new-reservation-modal.component.css']
})
export class NewReservationModalComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    public homeService: HomeService,
    public dialogRef: MatDialogRef<NewReservationModalComponent>,
    @Inject(MAT_DIALOG_DATA) public availableRooms) { }

  close(): void {
    this.dialogRef.close();
  }

  reservation = new Reservation();
  personInfo = new Person();
  persons: any;
  TotalCost: number;
  BaseTotalCost: number;
  enableNextStep: boolean;

  ngOnInit() {
    this.personInfo.SetDefaultValue();
    this.persons = new Array();
  }

  checkAvailableRooms() {
    if (this.reservation.StartDate === this.reservation.EndDate) {
      this.toastr.error('Checkout must be bigger than Checkin')
      return;
    }

    this.homeService.checkReservation(this.reservation).subscribe(response => {
      this.availableRooms = response;
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }

  SaveReservation() {
    this.reservation.HotelId = "3AB92D5C-33D1-4D17-83F3-A1CC5E00C4CD";
    this.reservation.Code = "RSV1098";
    this.reservation.UserId = "1DC97D96-BB38-4D7C-BCA3-111BADE204CB";
    this.reservation.TotalCost = this.TotalCost;

    this.homeService.saveReservation(this.reservation, this.persons).subscribe(response => {
      this.availableRooms = response;
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }

  AddGuest() {
    var room = this.availableRooms.find(e => e.id == this.reservation.RoomId);
    if (this.persons.length < room.totalBeds)
      this.persons.push(this.personInfo);
  }

  onClick() {
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

  resetRooms(){
    this.availableRooms = [];
    console.log("Reset rooms");
    
  }

  Discount(value){
    var discount = (this.BaseTotalCost * value) / 100;
    this.TotalCost = this.BaseTotalCost - discount;
  }
}
