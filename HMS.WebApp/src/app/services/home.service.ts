import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HomeService {

  constructor(private apiService : ApiService, private http: HttpClient) { }

  getReservations(){
    return this.http.get(this.apiService.getApiUrl() +
    'reservations');
  }
}
