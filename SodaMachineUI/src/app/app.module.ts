import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { HomeComponent } from './Home/Home.component';
import { AdminComponent } from './Admin/Admin.component';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { SodaCardComponent } from './home/soda-card/soda-card.component';
import { CashComponent } from './home/cash/cash.component';
import { SelectedSodaCardComponent } from './home/selected-soda-card/selected-soda-cardcomponent';
import { CoinDataService } from './services/data-services/coin-data.service';
import { SodaDataService } from './services/data-services/soda-data.service';
import { SodaService } from './services/data-manipulation-services/soda.service';
import { SodaMachineService } from './services/soda-machine.service';
import { CoinService } from './services/data-manipulation-services/coin.service';
import { OrderDataService } from './services/data-services/order-data.service';
import { AdminResolver } from './admin/resolver/admin.resolver';
import { ReactiveFormsModule } from '@angular/forms';
import { SodaAdminComponent } from './admin/soda-admin/soda-admin.component';
import { CoinAdminComponent } from './admin/coin-admin/coin-admin.component';

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      AdminComponent,
      SodaCardComponent,
      SelectedSodaCardComponent,
      CashComponent,
      CoinAdminComponent,
      SodaAdminComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      ReactiveFormsModule
   ],
   providers: [
       CoinDataService,
       CoinService,
       SodaDataService,
       SodaService,
       SodaMachineService,
       OrderDataService,
       AdminResolver
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
