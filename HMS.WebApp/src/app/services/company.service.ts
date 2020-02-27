import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(private apiService: ApiService, private http: HttpClient) { }

  getCompanies() {
    return this.http.get(this.apiService.getApiUrl() +
      'companies');
  }
}
