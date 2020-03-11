import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  constructor(private apiService: ApiService, private http: HttpClient) { }

  getAll() {
    return this.http.get(this.apiService.getApiUrl() +
      'persons');
  }

  add(person) {
    return this.http.post(this.apiService.getApiUrl() +
      'persons', JSON.stringify(person));
  }
}
