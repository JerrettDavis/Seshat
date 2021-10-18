import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent, UserPrintersHomePageModule} from './pages/home/home.component';
import {CreateUserPrinterComponent, CreateUserPrinterPageModule} from './pages/create-user-printer/create-user-printer.component';
import {UsersPrintersComponent, UsersPrintersPageModule} from './pages/users-printers/users-printers.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    children: [
      { path: '', component: UsersPrintersComponent, pathMatch: 'full', data: {animation: 'UserPrintersHomePage'} },
      { path: 'create', component: CreateUserPrinterComponent, data: {animation: 'UserPrintersCreatePage'} }
    ]
  },
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes),

    UserPrintersHomePageModule,
    UsersPrintersPageModule,
    CreateUserPrinterPageModule
  ]
})
export class UserPrintersRoutingModule { }
