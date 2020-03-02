import { Reservation } from './Reservation';
export class ReservationGroup {
  Group: Group;
  Reservation: Reservation;
}

export class Group {
  Id: string;
  Name: string;

  CompanyId: string;
}
