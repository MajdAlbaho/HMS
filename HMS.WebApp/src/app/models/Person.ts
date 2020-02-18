export class Person {
  FirstName: string;
  LastName: string;
  Nationality: number;
  Gender: boolean;
  IdNumber: number;

  RoomId: string;

  SetDefaultValue() {
    this.Nationality = 0;
    this.Gender = true;
  }
}
