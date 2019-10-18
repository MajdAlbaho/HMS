import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RoomService {

  constructor(private apiService : ApiService, private http: HttpClient) { }

  getRooms(){
    return this.http.get(this.apiService.getApiUrl() +
      'rooms');
  }
}
