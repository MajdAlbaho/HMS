import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private apiService : ApiService, private http: HttpClient) { }

  getGroups(){
    return this.http.get(this.apiService.getApiUrl() +
      'groups');
  }

  getGroupsByReservationStartDate(startReservationDate : string){
    return this.http.get(this.apiService.getApiUrl() +
    'groups/'+ startReservationDate);
  }
}
