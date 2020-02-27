import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Reservation } from '../models/Reservation';
import { Person } from '../models/Person';
import { Group } from '../models/Group';

@Injectable({
  providedIn: 'root'
})
export class ReservationService {

  constructor(private apiService: ApiService, private http: HttpClient) { }

  Get() {
    return this.http.get(this.apiService.getApiUrl() +
      'reservations');
  }

  Check(reservation: Reservation) {
    return this.http.post(this.apiService.getApiUrl() +
      'reservations/Check', JSON.stringify(reservation));
  }

  Save(reservation: Reservation, personInfo: Person, group: Group) {
    var param = {
      "Reservation": reservation,
      "Person": personInfo,
      "Group": group
    }

    return this.http.post(this.apiService.getApiUrl() +
      'reservations/SaveReservation', JSON.stringify(param));
  }

  Delete(Id) {
    return this.http.delete(this.apiService.getApiUrl() +
      'reservations/' + Id);
  }

  CheckIn(Id) {
    return this.http.post(this.apiService.getApiUrl() +
      'reservations/CheckIn', JSON.stringify(Id));
  }

  CheckOut(Id) {
    return this.http.post(this.apiService.getApiUrl() +
      'reservations/CheckOut', JSON.stringify(Id));
  }
}
