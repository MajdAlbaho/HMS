export class Person{
    FirstEnName : string;
    LastEnName : string;
    Nationality : number;
    Gender : boolean;
    IdNumber : number;

    RoomId : string;

    SetDefaultValue(){
        this.Nationality = 0;
        this.Gender = true;
    }
}