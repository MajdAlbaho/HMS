import { Component, OnInit } from '@angular/core';
import { RoomService } from 'src/app/services/room.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent implements OnInit {

  constructor(private roomService: RoomService, private toastr: ToastrService) { }

  hotelRooms: any;

  ngOnInit() {
    // ToDo : Filter by hotel
    this.roomService.getRooms().subscribe((response: any) => {
      var groups = new Set(response.map(item => item.floorNumber));
      this.hotelRooms = [];
      groups.forEach(g =>
        this.hotelRooms.push({
          floorNumber: g,
          rooms: response.sort().filter(i => i.floorNumber === g)
        }
        ));
    }
      , error => {
        this.toastr.error(error.message);
        console.log(error);
      });
  }

}
