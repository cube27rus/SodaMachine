import { Injectable, EventEmitter } from '@angular/core';
import { SodaDataService } from '../data-services/soda-data.service';
import { SodaModel } from '../../models/soda.model';
import { CoinModel } from '../../models/coin.model';
import { CoinDataService } from '../data-services/coin-data.service';

@Injectable({
  providedIn: 'root'
})
export class CoinService {
  coins: CoinModel[] = [];
  coinsChanged = new EventEmitter<CoinModel[]>();

  constructor(private coinsDataService: CoinDataService) {
    this.initCoins();
  }

  private initCoins() {
    this.coinsDataService.getAll().subscribe(
      (data: CoinModel[]) => {
        this.coins = data;
        this.coinsChanged.emit(data);
      }
    );
  }
}
