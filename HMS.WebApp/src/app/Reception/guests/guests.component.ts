import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { GuestService } from 'src/app/services/guest.service';
import { ToastrService } from 'ngx-toastr';
import { AddGuestModalComponent } from './add-guest-modal/add-guest-modal.component';


@Component({
  selector: 'app-guests',
  templateUrl: './guests.component.html',
  styleUrls: ['./guests.component.css']
})
export class GuestsComponent implements OnInit {

  guests : any;


  constructor(private guestService : GuestService, private toastr: ToastrService, public dialog: MatDialog) { }

  ngOnInit() {
    this.guestService.getGuests().subscribe(response => {
      this.guests = response
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }

  AddNewGuestModal(): void {
    this.dialog.open(AddGuestModalComponent, {
      width: '800px'
    }).afterClosed().subscribe(result => {
      if (typeof result !== 'undefined')
        this.guests.push(result);
    });
  }

}
