import {Component, NgModule, OnInit} from '@angular/core';

@Component({
  selector: 'app-users-printers-display',
  templateUrl: './users-printers-display.component.html',
  styleUrls: ['./users-printers-display.component.scss']
})
export class UsersPrintersDisplayComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}

@NgModule({
  declarations: [UsersPrintersDisplayComponent]
})
export class UsersPrinterDisplayModule {}
