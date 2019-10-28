import { Component, OnInit, Inject } from '@angular/core';
import { HomeService } from 'src/app/services/home.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Reservation } from 'src/app/models/Reservation';
import { Person } from 'src/app/models/Person';
import { Group } from '../../../models/Group';

@Component({
  selector: 'app-group-reservation-modal',
  templateUrl: './group-reservation-modal.component.html',
  styleUrls: ['./group-reservation-modal.component.css']
})
export class GroupReservationModalComponent implements OnInit {

  constructor(
    private homeService: HomeService, private toastr: ToastrService,
    public dialogRef: MatDialogRef<GroupReservationModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data) 
  { }

  close(): void {
    this.dialogRef.close();
  }
  ngOnInit() {
    this.personInfo.SetDefaultValue();
    this.persons = new Array();
    this.reservation.TotalCost = 0;
  }

  reservation = new Reservation();
  personInfo = new Person();
  group = new Group();

  persons: any;
  adultsHasError: boolean;
  availableRooms : any;
  TotalCost: number;
  BaseTotalCost: number;

  ValidateNumber(num) {
    if (num === undefined || num <= 0)
      this.adultsHasError = true;
    else
      this.adultsHasError = false;
  }

  checkReservation(){
    this.homeService.checkReservation(this.reservation).subscribe(response =>{      
      this.availableRooms = response;      
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }

  AddGuest(){
    this.personInfo.RoomId = this.reservation.RoomId;
    this.persons.push(Object.assign({} , this.personInfo));

    var room = this.availableRooms.find(e => e.id == this.reservation.RoomId);
    if(room == undefined)
      return;

    room.totalBeds = room.totalBeds - 1;

    if(room.totalBeds == 0)
      {        
        this.reservation.TotalCost = this.reservation.TotalCost + room.cost;

        let index = this.availableRooms.indexOf(room, 0);
        this.availableRooms.splice(index, 1);
        this.reservation.RoomId = null;
      }
  }

  SaveReservation() {
    console.log(this.reservation);
    this.reservation.RoomId = "";
    
    this.reservation.HotelId = "3AB92D5C-33D1-4D17-83F3-A1CC5E00C4CD";
    this.reservation.Code = "RSV1098";
    this.reservation.UserId = "1DC97D96-BB38-4D7C-BCA3-111BADE204CB";

    this.homeService.saveReservation(this.reservation, this.persons, this.group).subscribe(response => {
      
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }
}
