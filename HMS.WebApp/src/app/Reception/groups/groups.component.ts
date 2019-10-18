import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GroupService } from 'src/app/services/group.service';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  constructor(private groupService : GroupService, private toastr: ToastrService) { }
  groups : any;
  arrivalGroups : any;

  ngOnInit() {
    this.groupService.getGroups().subscribe(response => {
      this.groups = response;
    }
    ,error => {
      this.toastr.error(error.message);
      console.log(error);
    });

    var utc = new Date();
    utc.setDate(utc.getDate() + 1);
    var result = utc.toJSON().slice(0,10).replace(/-/g,'-');

    this.groupService.getGroupsByReservationStartDate(result).subscribe(response => {
      this.arrivalGroups = response;      
    }
    ,error => {
      this.toastr.error(error.message);
      console.log(error);
    });
    
  }

}
