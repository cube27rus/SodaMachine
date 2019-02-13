import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

constructor(private http: HttpClient) { }

  checkSecret(secret: string) {
    return this.http.get(environment.apiUrl + `/api/admin/${secret}`);
  }

}
