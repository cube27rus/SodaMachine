import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { SodaModel } from '../../models/soda.model';

@Injectable({
  providedIn: 'root'
})
export class SodaDataService {

constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(environment.apiUrl + '/api/sodas');
  }

  put(id: number, soda: SodaModel) {
    return this.http.put(environment.apiUrl + `/api/sodas/${id}`, soda);
  }

  post( soda: SodaModel) {
    return this.http.post(environment.apiUrl + `/api/sodas/`, soda);
  }

  delete(id: number) {
    return this.http.delete(environment.apiUrl + `/api/sodas//${id}`);
  }

}
