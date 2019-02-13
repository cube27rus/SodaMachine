import { Component, OnInit } from '@angular/core';
import { SodaMachineService } from '../services/soda-machine.service';
import { SodaModel } from '../models/soda.model';
import { SodaService } from '../services/data-manipulation-services/soda.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  sodas: SodaModel[] = [];
  selectedSodas: SodaModel[] = [];

  constructor(private sodaMachineService: SodaMachineService,
    private sodaService: SodaService) { }

  ngOnInit() {
    this.initSodas();
    this.syncSodas();
  }

  public syncSodas() {
    this.sodaMachineService.selectedSodasChanged.subscribe(
      (data: SodaModel[]) => {
        this.selectedSodas = data;
      }
    );
  }

  public madeOrder() {
    const totalOrderAmount = this.selectedSodas.map(x => x.price).reduce((sum, current) => sum + current, 0);
    const totalCoinAmount = this.sodaMachineService.pushedCoins.map(x => x.value).reduce((sum, current) => sum + current, 0);
    if  (totalCoinAmount < totalOrderAmount){
      alert('Недостаточно средств');
    } else {
      this.sodaMachineService.madeOrder();
    }
  }

  private initSodas() {
    this.sodas = this.sodaService.sodas;
    this.sodaService.sodasChanged.subscribe(
        (data: SodaModel[]) => {
          this.sodas = data;
        }
    );
  }
}
