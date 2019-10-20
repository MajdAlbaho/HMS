import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog} from '@angular/material';
import { Reservation } from 'src/app/models/reversation';
import { HomeService } from 'src/app/services/home.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-check-reservation-modal',
  templateUrl: './check-reservation-modal.component.html',
  styleUrls: ['./check-reservation-modal.component.css']
})
export class CheckReservationModalComponent implements OnInit {

  constructor(
    private homeService: HomeService, private toastr: ToastrService,
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

  ValidateNumber(num) {
    if (num === undefined || num <= 0)
      this.adultsHasError = true;
    else
      this.adultsHasError = false;
  }

  checkReservation(){
    this.homeService.checkReservation(this.reservation).subscribe(response =>{      
      
      this.dialogRef.close();
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }
  
}
