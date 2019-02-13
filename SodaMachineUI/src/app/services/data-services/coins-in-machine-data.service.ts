import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { CoinsInMachineModel } from 'src/app/models/coins-in-machine.model';

@Injectable({
  providedIn: 'root'
})
export class CoinsInMachineDataService {

constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get(environment.apiUrl + '/api/coinsinmachines');
  }

  put(id: number, coin: CoinsInMachineModel) {
    return this.http.put(environment.apiUrl + `/api/coinsinmachines/${id}`, coin);
  }

}
