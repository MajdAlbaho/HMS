export class Reservation {
  id: string;
  code: string;
  HotelId: string;
  StartDate: Date;
  EndDate: Date;
  UserId: string;
  TotalCost: number;

  Adults: number
  RoomType: number;
  RoomId: string;
  GroupName: string;
}
