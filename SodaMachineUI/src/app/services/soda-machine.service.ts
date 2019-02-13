import { Injectable, EventEmitter } from '@angular/core';
import { SodaModel } from '../models/soda.model';
import { CoinModel } from '../models/coin.model';
import { SodaService } from './data-manipulation-services/soda.service';
import { OrderDataService } from './data-services/order-data.service';
import { OrderRequst } from '../models/request/order-request';
import { CoinsInMachineService } from './data-manipulation-services/coins-in-machine.service';

@Injectable({
  providedIn: 'root'
})
export class SodaMachineService {
  pushedCoins: CoinModel[] = [];
  pushedCoinsChanged = new EventEmitter<CoinModel[]>();
  selectedSodas: SodaModel[] = [];
  selectedSodasChanged = new EventEmitter<SodaModel[]>();

  constructor(private sodaService: SodaService,
    private orderDataService: OrderDataService,
    private coinInMachineService: CoinsInMachineService) {
  }

  public addSoda(soda: SodaModel) {
    this.selectedSodas.push(soda);
    this.selectedSodasChanged.emit(this.selectedSodas);
    this.sodaService.reduceCount(soda);
  }

  public pushCoin(coin: CoinModel) {
    this.pushedCoins.push(coin);
    this.pushedCoinsChanged.emit(this.pushedCoins);
  }

  public madeOrder() {
    const order: OrderRequst = new OrderRequst();
    order.coins = this.pushedCoins;
    order.sodas = this.selectedSodas;
    this.orderDataService.processOrder(order).subscribe(
      (data: CoinModel[]) => {
        alert('Ваша сдача: ' + data.map(x => x.name).join(', '));
        this.coinInMachineService.initCoinsInMachine();
        this.sodaService.initSodas();
        this.coinInMachineService.initCoinsInMachine();
        this.reset();
      }
    );
  }

  public reset() {
    this.selectedSodas.length = 0;
    this.selectedSodasChanged.emit(this.selectedSodas);
    this.pushedCoins.length = 0;
    this.pushedCoinsChanged.emit(this.pushedCoins);
  }
}
