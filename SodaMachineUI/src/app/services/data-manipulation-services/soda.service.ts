import { Injectable, EventEmitter } from '@angular/core';
import { SodaDataService } from '../data-services/soda-data.service';
import { SodaModel } from '../../models/soda.model';

@Injectable({
  providedIn: 'root'
})
export class SodaService {
  sodas: SodaModel[] = [];
  sodasChanged = new EventEmitter<SodaModel[]>();

  constructor(private sodaDataService: SodaDataService) {
    this.initSodas();
  }

  public reduceCount(soda: SodaModel) {
    const index = this.sodas.findIndex(x => x.id === soda.id);
    this.sodas[index].amount--;
    this.sodasChanged.emit(this.sodas);
  }

  public initSodas() {
    this.sodaDataService.getAll().subscribe(
      (data: SodaModel[]) => {
        this.sodas = data;
        this.sodasChanged.emit(data);
      }
    );
  }
}
