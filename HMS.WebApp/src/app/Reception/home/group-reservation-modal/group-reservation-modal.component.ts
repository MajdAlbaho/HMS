import { Component, OnInit, Inject } from '@angular/core';
import { ReservationService } from 'src/app/services/Reservation.service';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialog } from '@angular/material';
import { Reservation } from 'src/app/models/Reservation';
import { Person } from 'src/app/models/Person';
import { Group, ReservationGroup } from '../../../models/Group';
import { AddGroupModalComponent } from '../../groups/add-group-modal/add-group-modal.component';
import { GroupService } from '../../../services/group.service';
import { PersonService } from '../../../services/person.service';

@Component({
  selector: 'app-group-reservation-modal',
  templateUrl: './group-reservation-modal.component.html',
  styleUrls: ['./group-reservation-modal.component.css']
})
export class GroupReservationModalComponent implements OnInit {

  constructor(
    private reservationService: ReservationService, private toastr: ToastrService, private groupService: GroupService,
    public dialogRef: MatDialogRef<GroupReservationModalComponent>,
    public personService: PersonService,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data) { }

  close(): void {
    this.dialogRef.close();
  }
  ngOnInit() {
    this.personInfo.SetDefaultValue();
    this.selectedPersons = new Array();
    this.personService.getAll().subscribe((response: Person[]) => {
      this.personsList = response;
    });
    this.reservation.TotalCost = 0;

    this.groupService.getGroups().subscribe((response: Group[]) => {
      this.groups = response
    }, error => {
      this.toastr.error(error.error.message);
      this.toastr.error(error.message);
      console.log(error);
    });

    if (this.data !== null) {
      this.reservation.StartDate = this.data.StartDate;
      this.reservation.EndDate = this.data.EndDate;
      this.reservation.Adults = this.data.Adults;
    }
  }

  reservation = new Reservation();
  personInfo = new Person();
  group = new Group();

  adultsHasError: boolean;
  availableRooms: any;
  TotalCost: number = 0;
  BaseTotalCost: number;
  Days: number;
  DiscountValue: number = 0;
  personsList: Person[];
  selectedPersons: any;
  selectedPerson: Person;

  activeState: string;

  groups: Group[];

  ValidateNumber(num) {
    this.adultsHasError = num === undefined || num <= 0;
  }

  checkReservation() {
    this.reservationService.Check(this.reservation).subscribe(response => {
      this.availableRooms = response;
    }, error => {
      this.toastr.error(error.error.message);
      console.log(error);
    });
  }

  AddSelectedPerson() {
    if (typeof (this.selectedPerson) === 'undefined' || this.selectedPerson == null
      || this.reservation.RoomId == null)
      return;

    this.selectedPerson.RoomId = this.reservation.RoomId;
    this.selectedPersons.push(Object.assign({}, this.selectedPerson));
    this.personsList.splice(this.personsList.indexOf(this.selectedPerson), 1);

    var room = this.availableRooms.find(e => e.id == this.reservation.RoomId);
    if (room == undefined)
      return;

    room.totalBeds = room.totalBeds - 1;

    if (this.TotalCost > 0)
      this.Discount(0);
    this.TotalCost = this.TotalCost + (room.cost * this.Days);
    this.BaseTotalCost = this.TotalCost;
    this.Discount(this.DiscountValue);

    if (room.totalBeds == 0) {
      let index = this.availableRooms.indexOf(room, 0);
      this.availableRooms.splice(index, 1);
      this.reservation.RoomId = null;
    }

    this.selectedPerson = null;
  }

  AddNewGroup() {
    this.dialog.open(AddGroupModalComponent, {
      width: '350px'
    }).afterClosed().subscribe(result => {

    });
  }

  Discount(value) {
    var discount = (this.BaseTotalCost * value) / 100;
    this.TotalCost = this.BaseTotalCost - discount;
  }

  Next() {
    this.Days = this.date_diff_indays(this.reservation.StartDate, this.reservation.EndDate);
  }

  date_diff_indays(date1, date2) {
    var dt1 = new Date(date1);
    var dt2 = new Date(date2);
    return Math.floor((Date.UTC(dt2.getFullYear(), dt2.getMonth(), dt2.getDate()) - Date.UTC(dt1.getFullYear(), dt1.getMonth(), dt1.getDate())) / (1000 * 60 * 60 * 24));
  }

  SaveReservation() {
    this.reservation.HotelId = "3AB92D5C-33D1-4D17-83F3-A1CC5E00C4CD";
    this.reservation.code = "RSV1098";
    this.reservation.UserId = "47009186-d2a8-426d-8ad7-af784ee8bb5d";
    this.reservation.TotalCost = this.TotalCost;

    this.reservation.ReservationGroups = [];
    var item = new ReservationGroup();
    item.Group = this.group;
    this.reservation.ReservationGroups.push(item);

    this.reservationService.Save(this.reservation, this.selectedPersons)
      .subscribe(response => {
        this.dialogRef.close(response);
      }, error => {
        this.toastr.error(error.error.message);
        this.toastr.error(error.message);
        console.log(error);
      });
  }

  setSelectedPerson(person) {
    this.selectedPerson = person;
    this.activeState = person.id;
  }
}
