import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { GroupService } from 'src/app/services/group.service';
import { MatDialog } from '@angular/material';
import { AddGroupModalComponent } from './add-group-modal/add-group-modal.component';
import { AddCompanyModalComponent } from './add-company-modal/add-company-modal.component';



@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  constructor(private groupService: GroupService, private toastr: ToastrService, public dialog: MatDialog) { }
  groups: any;
  arrivalGroups: any;

  ngOnInit() {
    this.groupService.getGroups().subscribe(response => {
      this.groups = response;
    }
      , error => {
        this.toastr.error(error.message);
        console.log(error);
      });

    var utc = new Date();
    utc.setDate(utc.getDate() + 1);
    var result = utc.toJSON().slice(0, 10).replace(/-/g, '-');

    this.groupService.getGroupsByReservationStartDate(result).subscribe(response => {
      this.arrivalGroups = response;
    }
      , error => {
        this.toastr.error(error.message);
        console.log(error);
      });

  }

  AddNewGroupModal(): void {
    this.dialog.open(AddGroupModalComponent, {
      width: '800px'
    }).afterClosed().subscribe(result => {
      if (typeof result !== 'undefined')
        this.groups.push(result);
    });
  }

  AddNewCompanyModal() {
    this.dialog.open(AddCompanyModalComponent, {
      width: '800px'
    }).afterClosed().subscribe(result => {
      if (typeof result !== 'undefined')
        this.groups.push(result);
    });
  }

}
