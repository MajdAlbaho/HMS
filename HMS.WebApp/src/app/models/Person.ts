export class Person{
    FirstName : string;
    LastName : string;
    Nationality : number;
    Gender : number;
    IdNumber : number;

    SetDefaultValue(){
        this.Nationality = this.Gender = 0;
    }
}