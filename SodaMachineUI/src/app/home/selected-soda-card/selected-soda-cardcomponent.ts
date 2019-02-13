import { Component, OnInit, Input } from '@angular/core';
import { SodaModel } from '../../models/soda.model';

@Component({
  selector: 'app-selected-soda-card',
  templateUrl: './selected-soda-card.component.html',
  styleUrls: ['./selected-soda-card.component.css']
})
export class SelectedSodaCardComponent implements OnInit {
  @Input() soda: SodaModel;

  constructor() { }

  ngOnInit() {
  }
}
