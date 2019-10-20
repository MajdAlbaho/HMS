import { Component, OnInit, Inject } from '@angular/core';
import { HomeService } from 'src/app/services/home.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

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
  }

}
