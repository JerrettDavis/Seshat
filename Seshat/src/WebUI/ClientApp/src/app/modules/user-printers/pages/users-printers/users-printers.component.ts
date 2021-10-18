import {Component, NgModule, OnInit} from '@angular/core';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {RouterModule} from '@angular/router';

@Component({
  selector: 'app-users-printers',
  templateUrl: './users-printers.component.html',
  styleUrls: ['./users-printers.component.scss']
})
export class UsersPrintersComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

@NgModule({
  imports: [
    MatButtonModule,
    MatIconModule,
    RouterModule
  ],
  declarations: [UsersPrintersComponent]
})
export class UsersPrintersPageModule {}
