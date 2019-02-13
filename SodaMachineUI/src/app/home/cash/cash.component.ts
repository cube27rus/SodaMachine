import { Component, OnInit } from '@angular/core';
import { CoinModel } from '../../models/coin.model';
import { SodaMachineService } from '../../services/soda-machine.service';
import { CoinService } from '../../services/data-manipulation-services/coin.service';
import { CoinsInMachineService } from '../../services/data-manipulation-services/coins-in-machine.service';
import { CoinsInMachineModel } from '../../models/coins-in-machine.model';

@Component({
  selector: 'app-cash',
  templateUrl: './cash.component.html',
  styleUrls: ['./cash.component.css']
})
export class CashComponent implements OnInit {
  coins: CoinModel[] = [];
  pushedCoins: CoinModel[] = [];
  coinsInMachine: CoinsInMachineModel[] = [];
  totalAmount = 0;

  constructor(private sodaMachineService: SodaMachineService,
    private coinsService: CoinService,
    private coinsInMachineService: CoinsInMachineService) { }

  ngOnInit() {
    this.initCoins();
  }

  private initCoins() {
    // разновидности монет
    this.coins = this.coinsService.coins;
    this.coinsService.coinsChanged.subscribe(
      (data: CoinModel[]) => {
        this.coins = data;
      }
    );

    // монеты уже в автомате
    this.coinsInMachine = this.coinsInMachineService.coinsInMachine;
    this.coinsInMachineService.coinsInMachineChanged.subscribe(
      (data: CoinsInMachineModel[]) => {
        this.coinsInMachine = data;
      }
    );

    // монеты брошенные пользователем в автомат
    this.pushedCoins = this.sodaMachineService.pushedCoins;
    this.sodaMachineService.pushedCoinsChanged.subscribe(
      (data: CoinModel[]) => {
        this.pushedCoins = data;
        this.calculateTotal();
      }
    );
  }

  public pushCoin(coin: CoinModel) {
    if (coin.isAvalible) {
      this.sodaMachineService.pushCoin(coin);
    }
  }

  public calculateTotal() {
    this.totalAmount = this.pushedCoins.map(x => x.value).reduce(function(a, b) { return a + b; }, 0);
  }

}
