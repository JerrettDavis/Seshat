import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent, UserPrintersHomePageModule} from './pages/home/home.component';
import {CreateUserPrinterComponent, CreateUserPrinterPageModule} from './pages/create-user-printer/create-user-printer.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'create', component: CreateUserPrinterComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),

    UserPrintersHomePageModule,
    CreateUserPrinterPageModule
  ]
})
export class UserPrintersRoutingModule { }
