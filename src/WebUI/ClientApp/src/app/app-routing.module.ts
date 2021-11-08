import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule)
  },
  {
    path: 'users/printers',
    loadChildren: () => import('./modules/user-printers/user-printers.module').then(m => m.UserPrintersModule)
  },
  {
    path: 'manufacturers',
    loadChildren: () => import('./modules/manufacturers/manufacturers.module').then(m => m.ManufacturersModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
