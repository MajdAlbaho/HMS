import { Component, OnInit } from '@angular/core';
import { Person } from 'src/app/models/Person';
import { PersonService } from '../../../services/person.service';
import { MatDialogRef } from '@angular/material';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-add-person-modal',
  templateUrl: './add-person-modal.component.html',
  styleUrls: ['./add-person-modal.component.css']
})
export class AddPersonModalComponent implements OnInit {

  constructor(public personService: PersonService,
    public dialogRef: MatDialogRef<AddPersonModalComponent>, public toast: ToastrService) { }

  personInfo = new Person();

  ngOnInit() {
    this.personInfo.SetDefaultValue();

  }

  AddPerson() {
    console.log(this.personInfo);

    this.personService.add(this.personInfo).subscribe(response => {
      this.toast.success('Added');
      this.dialogRef.close(response);
    });
  }

  close(): void {
    this.dialogRef.close();
  }
}
