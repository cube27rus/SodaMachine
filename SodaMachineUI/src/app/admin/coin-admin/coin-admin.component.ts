import { Component, OnInit } from '@angular/core';
import { CoinsInMachineModel } from 'src/app/models/coins-in-machine.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CoinsInMachineService } from 'src/app/services/data-manipulation-services/coins-in-machine.service';
import { CoinsInMachineDataService } from 'src/app/services/data-services/coins-in-machine-data.service';

@Component({
  selector: 'app-coin-admin',
  templateUrl: './coin-admin.component.html',
  styleUrls: ['./coin-admin.component.css']
})
export class CoinAdminComponent implements OnInit {
  coinsInMachine: CoinsInMachineModel[] = [];
  public editCoinForm: FormGroup = null;

  constructor(private coinsInMachineDataService: CoinsInMachineDataService,
    private formBuilder: FormBuilder,
    private coinsInMachineService: CoinsInMachineService) {}


  ngOnInit() {
    this.initCoins();
  }

  public initEditForm(coin: CoinsInMachineModel) {
    this.editCoinForm = this.formBuilder.group({
      id: [coin.id, Validators.required],
      count: [coin.count, Validators.required],
      enabled: [coin.coin.isAvalible, [Validators.required]]
    });
  }

  public putCoin() {
    if (this.editCoinForm.valid) {
      const coin = this.editCoinForm.getRawValue();
      const currentCoin = this.coinsInMachine.find(x => x.id === coin.id);
      currentCoin.count = coin.count;
      currentCoin.coin.isAvalible = coin.enabled;
      this.coinsInMachineDataService.put(currentCoin.id, currentCoin).subscribe(
        data => {
          this.editCoinForm = null;
          this.coinsInMachineService.initCoinsInMachine();
        },
        error => console.log(error)
      );
    }
  }

  private initCoins() {
    this.coinsInMachine = this.coinsInMachineService.coinsInMachine;
    this.coinsInMachineService.coinsInMachineChanged.subscribe(
      (data: CoinsInMachineModel[]) => {
        this.coinsInMachine = data;
        console.log(this.coinsInMachine);
      }
    );
  }
}
