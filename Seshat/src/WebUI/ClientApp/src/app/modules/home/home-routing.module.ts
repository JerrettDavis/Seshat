import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent, HomePageModule} from './pages/home/home.component';


export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes),

    HomePageModule
  ],
  exports: [RouterModule],
})
export class HomeRoutingModule { }
