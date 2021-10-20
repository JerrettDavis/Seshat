import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersPrintersDisplayComponent } from './users-printers-display.component';

describe('UsersPrintersDisplayComponent', () => {
  let component: UsersPrintersDisplayComponent;
  let fixture: ComponentFixture<UsersPrintersDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersPrintersDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersPrintersDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
