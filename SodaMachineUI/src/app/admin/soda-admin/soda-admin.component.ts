import { Component, OnInit } from '@angular/core';
import { SodaModel } from 'src/app/models/soda.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SodaService } from 'src/app/services/data-manipulation-services/soda.service';
import { SodaDataService } from 'src/app/services/data-services/soda-data.service';

@Component({
  selector: 'app-soda-admin',
  templateUrl: './soda-admin.component.html',
  styleUrls: ['./soda-admin.component.css']
})
export class SodaAdminComponent implements OnInit {
  sodas: SodaModel[] = [];
  public newSodaForm: FormGroup = null;
  public editSodaForm: FormGroup = null;

  constructor(private sodaService: SodaService,
    private sodaDataService: SodaDataService,
    private formBuilder: FormBuilder) {
    }

  ngOnInit() {
    this.initSodas();
    this.newSodaForm = this.initSodaForm();
  }

  public addSoda() {
    if (this.newSodaForm.valid) {
      const newSoda = this.newSodaForm.getRawValue();
      this.sodaDataService.post(newSoda).subscribe(
        data => {
          this.newSodaForm = this.initSodaForm();
          this.sodaService.initSodas();
        },
        error => console.log(error)
      );
    }
  }

  public putSoda() {
    if (this.editSodaForm.valid) {
      const newSoda = this.editSodaForm.getRawValue();
      this.sodaDataService.put(newSoda.id, newSoda).subscribe(
        data => {
          this.editSodaForm = null;
          this.sodaService.initSodas();
        },
        error => console.log(error)
      );
    }
  }

  public initEditForm(soda: SodaModel) {
    this.editSodaForm = this.formBuilder.group({
      id: [soda.id, Validators.required],
      name: [soda.name, Validators.required],
      img: [soda.img, [Validators.required]],
      price: [soda.price, [Validators.required]],
      amount: [soda.amount, [Validators.required]],
    });
  }

  private initSodas() {
    this.sodas = this.sodaService.sodas;
    this.sodaService.sodasChanged.subscribe(
        (data: SodaModel[]) => {
          this.sodas = data;
        }
    );
  }

  private initSodaForm() {
    return this.formBuilder.group({
      name: ['', Validators.required],
      img: ['', [Validators.required]],
      price: ['', [Validators.required]],
      amount: ['', [Validators.required]],
    });
  }

}
