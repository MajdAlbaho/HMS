import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Reservation } from '../models/Reservation';
import { Person } from '../models/Person';
import { Group } from '../models/Group';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private apiService: ApiService, private http: HttpClient) { }

  getReservations() {
    return this.http.get(this.apiService.getApiUrl() +
      'reservations');
  }

  checkReservation(reservation: Reservation) {
    return this.http.post(this.apiService.getApiUrl() +
      'reservations/Check', JSON.stringify(reservation));
  }

  saveReservation(reservation: Reservation, personInfo: Person, group : Group) {
    var param = {
      "Reservation": reservation,
      "Person": personInfo,
      "Group" : group
    }   

    return this.http.post(this.apiService.getApiUrl() +
      'reservations', JSON.stringify(param));
  }
}
