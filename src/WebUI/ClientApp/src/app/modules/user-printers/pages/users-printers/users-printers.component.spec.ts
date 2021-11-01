import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersPrintersComponent } from './users-printers.component';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {RouterTestingModule} from '@angular/router/testing';

describe('UsersPrintersComponent', () => {
  let component: UsersPrintersComponent;
  let fixture: ComponentFixture<UsersPrintersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersPrintersComponent ],
      imports: [
        MatButtonModule,
        MatIconModule,
        RouterTestingModule
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersPrintersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
