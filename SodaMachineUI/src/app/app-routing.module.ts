import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { HomeComponent } from './Home/Home.component';
import { AdminComponent } from './Admin/Admin.component';
import { NgModule } from '@angular/core';
import { AdminResolver } from './admin/resolver/admin.resolver';

const routes: Routes = [
  { path: 'admin/:secret' , component: AdminComponent, resolve: {AdminResolver}},
  { path: '**', component: HomeComponent}
];

@NgModule({
  imports: [
      RouterModule.forRoot(routes, {preloadingStrategy: PreloadAllModules})
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
