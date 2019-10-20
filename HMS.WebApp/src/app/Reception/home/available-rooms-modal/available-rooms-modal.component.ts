import { Component, OnInit, Inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-available-rooms-modal',
  templateUrl: './available-rooms-modal.component.html',
  styleUrls: ['./available-rooms-modal.component.css']
})
export class AvailableRoomsModalComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<AvailableRoomsModalComponent>,
    @Inject(MAT_DIALOG_DATA) public availableRooms,
    @Inject(MAT_DIALOG_DATA) public sourceDialog)
  { }

  ngOnInit() {
  }

  close(): void {
    this.dialogRef.close();
  }

  newReservation(){
    this.dialogRef.close();

    
  }

}
