import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { OrderRequst } from '../../models/request/order-request';

@Injectable({
  providedIn: 'root'
})
export class OrderDataService {

constructor(private http: HttpClient) { }

  processOrder(request: OrderRequst) {
    return this.http.post(environment.apiUrl + '/api/order', request);
  }

}
