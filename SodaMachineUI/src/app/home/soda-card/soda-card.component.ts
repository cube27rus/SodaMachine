import { Component, Input } from '@angular/core';
import { SodaModel } from '../../models/soda.model';
import { SodaMachineService } from '../../services/soda-machine.service';

@Component({
  selector: 'app-soda-card',
  templateUrl: './soda-card.component.html',
  styleUrls: ['./soda-card.component.css']
})
export class SodaCardComponent {
  @Input() soda: SodaModel;

  constructor(private sodaMachineService: SodaMachineService) { }

  public selectSoda(soda: SodaModel) {
    const pushedCoins = this.sodaMachineService.pushedCoins.map(x => x.value).reduce(function(a, b) { return a + b; }, 0);
    let selectedSodasPrice = this.sodaMachineService.selectedSodas.map(x => x.price).reduce(function(a, b) { return a + b; }, 0);
    selectedSodasPrice += soda.price;

    if (soda.amount > 0 && selectedSodasPrice <= pushedCoins) {
      this.sodaMachineService.addSoda(soda);
    } else {
      alert('Товар закончился или недостаточно средств.');
    }
  }
}
