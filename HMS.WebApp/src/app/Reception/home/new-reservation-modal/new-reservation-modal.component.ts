import { Component, OnInit, Inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Reservation } from 'src/app/models/Reservation';
import { HomeService } from 'src/app/services/home.service';
import { Person } from 'src/app/models/Person';

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
    @Inject(MAT_DIALOG_DATA) public availableRooms) 
  { }

  close(): void {
    this.dialogRef.close();
  }

  reservation = new Reservation();
  personInfo = new Person();


  ngOnInit() {
    this.personInfo.SetDefaultValue();    
  }

  checkAvailableRooms(){
    this.homeService.checkReservation(this.reservation).subscribe(response => {
      this.availableRooms = response;
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }

  SaveReservation(){

  }

}
