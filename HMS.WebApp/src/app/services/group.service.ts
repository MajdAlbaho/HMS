import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';
import { Group } from '../models/Group';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  constructor(private apiService: ApiService, private http: HttpClient) { }

  getGroups() {
    return this.http.get(this.apiService.getApiUrl() +
      'groups');
  }

  getGroupsByReservationStartDate(startReservationDate: string) {
    return this.http.get(this.apiService.getApiUrl() +
      'groups/' + startReservationDate);
  }

  save(group: Group) {
    return this.http.post(this.apiService.getApiUrl() +
      'groups/SaveGroup', JSON.stringify(group));
  }
}
