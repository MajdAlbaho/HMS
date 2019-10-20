import { Component, OnInit, Inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Reservation } from 'src/app/models/Reservation';

@Component({
  selector: 'app-new-reservation-modal',
  templateUrl: './new-reservation-modal.component.html',
  styleUrls: ['./new-reservation-modal.component.css']
})
export class NewReservationModalComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<NewReservationModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data) 
  { }

  close(): void {
    this.dialogRef.close();
  }

  reservation = new Reservation();

  ngOnInit() {
  }

}
