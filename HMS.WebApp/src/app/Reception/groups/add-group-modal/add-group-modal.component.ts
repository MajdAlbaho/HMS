import { Component, OnInit, Inject } from '@angular/core';
import { Group } from '../../../models/Group';
import { CompanyService } from '../../../services/company.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { AddCompanyComponent } from '../../../Management/companies/add-company/add-company.component';
import { GroupService } from '../../../services/group.service';

@Component({
  selector: 'app-add-group-modal',
  templateUrl: './add-group-modal.component.html',
  styleUrls: ['./add-group-modal.component.css']
})
export class AddGroupModalComponent implements OnInit {

  constructor(private companyService: CompanyService, private toastr: ToastrService, private groupService: GroupService,
    public dialogRef: MatDialogRef<AddGroupModalComponent>, public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data) { }

  group = new Group();
  companies: any;

  ngOnInit() {
    this.companyService.getCompanies().subscribe(response => {
      this.companies = response;
      console.log(this.companies);

    }, error => {
      this.toastr.error(error.error.message);
      console.log(error);
    });
  }

  AddGroup() {
    this.groupService.save(this.group).subscribe(response => {
      this.companies = response;
      console.log(this.companies);

    }, error => {
      this.toastr.error(error.error.message);
      console.log(error);
    });
  }

  AddNewCompany() {
    this.dialog.open(AddCompanyComponent, {
      width: '350px'
    }).afterClosed().subscribe(result => {

    });
  }

  close(): void {
    this.dialogRef.close();
  }
}
