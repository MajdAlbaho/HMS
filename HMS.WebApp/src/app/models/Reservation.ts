export class Reservation {
  id: string;
  code: string;
  HotelId: string;
  StartDate: Date;
  EndDate: Date;
  UserId: string;
  TotalCost: number;
  statusId: number;

  Adults: number
  RoomType: number;
  RoomId: string;
  GroupName: string;

  status: Status;
}

export class Status {
  enName: string;
}
