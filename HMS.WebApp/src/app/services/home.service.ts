import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { checkReservation } from '../models/CheckReservation';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private apiService : ApiService, private http: HttpClient) { }

  getReservations(){
    return this.http.get(this.apiService.getApiUrl() +
      'reservations');
  }

  checkReservation(reservation : checkReservation){
    return this.http.post(this.apiService.getApiUrl() +
      'reservations/Check',JSON.stringify(reservation));
  }
}
