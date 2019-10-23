import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog} from '@angular/material';
import { Reservation } from 'src/app/models/Reservation';
import { HomeService } from 'src/app/services/home.service';
import { ToastrService } from 'ngx-toastr';
import { NewReservationModalComponent } from '../new-reservation-modal/new-reservation-modal.component';
import { GroupReservationModalComponent } from '../group-reservation-modal/group-reservation-modal.component';

@Component({
  selector: 'app-check-reservation-modal',
  templateUrl: './check-reservation-modal.component.html',
  styleUrls: ['./check-reservation-modal.component.css']
})
export class CheckReservationModalComponent implements OnInit {

  constructor(
    private homeService: HomeService, private toastr: ToastrService,public dialog: MatDialog,
    public dialogRef: MatDialogRef<CheckReservationModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data) 
  { }

  close(): void {
    this.dialogRef.close();
  }

  ngOnInit() {
  }

  reservation = new Reservation();
  adultsHasError: boolean;
  availableRooms : any;

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

  IndividualReservationModal(): void {
    var avail = this.availableRooms;
    this.dialog.open(NewReservationModalComponent, {
      width: '800px',
      data: avail
    });

    this.dialogRef.close();
  }

  GroupReservationModal(): void {
    this.dialog.open(GroupReservationModalComponent, {
      width: '800px',
      data: { }
    });

    this.dialogRef.close();
  }
  
}
