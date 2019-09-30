import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor() { }

  getApiUrl() {
    return 'http://localhost:51071/api/';
  }
}


// let header = new HttpHeaders();
//     header = header.append('content-type', 'application/json');

// return this.http.post(this.apiService.getApiUrl() + 'Teams/SendInvitation/',
// JSON.stringify(invitiation), { headers: header });
