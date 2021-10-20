import {Component, NgModule, OnInit} from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {RouterModule, RouterOutlet} from '@angular/router';
import {routeAnimations} from '../../animations/page-transitions';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  animations: [
    routeAnimations
  ]
})
export class HomeComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  prepareRoute(outlet: RouterOutlet) {
    return outlet?.activatedRouteData?.animation;
  }

}

@NgModule({
  imports: [
    MatIconModule,
    MatButtonModule,
    RouterModule
  ],
  declarations: [HomeComponent]
})
export class UserPrintersHomePageModule {}
