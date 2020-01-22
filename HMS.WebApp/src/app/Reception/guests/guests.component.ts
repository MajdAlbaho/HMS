import { Component, OnInit } from '@angular/core';
import { GuestService } from 'src/app/services/guest.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-guests',
  templateUrl: './guests.component.html',
  styleUrls: ['./guests.component.css']
})
export class GuestsComponent implements OnInit {

  guests : any;


  constructor(private guestService : GuestService, private toastr: ToastrService) { }

  ngOnInit() {
    this.guestService.getGuests().subscribe(response => {
      this.guests = response
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });
  }

}
