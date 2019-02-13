import { Injectable, EventEmitter } from '@angular/core';
import { CoinsInMachineDataService } from '../data-services/coins-in-machine-data.service';
import { CoinsInMachineModel } from '../../models/coins-in-machine.model';

@Injectable({
  providedIn: 'root'
})
export class CoinsInMachineService {
    coinsInMachine: CoinsInMachineModel[] = [];
    coinsInMachineChanged = new EventEmitter<CoinsInMachineModel[]>();

  constructor(private coinsInMachineDataService: CoinsInMachineDataService) {
    this.initCoinsInMachine();
  }

  public initCoinsInMachine() {
    this.coinsInMachineDataService.getAll().subscribe(
      (data: CoinsInMachineModel[]) => {
        this.coinsInMachine = data;
        this.coinsInMachineChanged.emit(data);
      }
    );
  }
}
