import { Component, OnInit, Inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Reservation } from 'src/app/models/Reservation';
import { ReservationService } from 'src/app/services/Reservation.service';
import { Person } from 'src/app/models/Person';
import { LoginComponent } from '../../../auth/user/login/login.component';


@Component({
  selector: 'app-add-company-modal',
  templateUrl: './add-company-modal.component.html',
  styleUrls: ['./add-company-modal.component.css']
})
export class AddCompanyModalComponent implements OnInit {

  constructor(
    private toastr: ToastrService,
    public reservationService: ReservationService,
    public dialogRef: MatDialogRef<AddCompanyModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data) { }

  close(): void {
    this.dialogRef.close();
  }

  reservation = new Reservation();
  personInfo = new Person();
  persons: any;
  TotalCost: number;
  BaseTotalCost: number;
  enableNextStep: boolean;
  availableRooms: any;

  ngOnInit() {
    this.personInfo.SetDefaultValue();
    this.persons = new Array();

    if (this.data !== null) {
      this.reservation.StartDate = this.data.StartDate;
      this.reservation.EndDate = this.data.EndDate;
    }
  }



}