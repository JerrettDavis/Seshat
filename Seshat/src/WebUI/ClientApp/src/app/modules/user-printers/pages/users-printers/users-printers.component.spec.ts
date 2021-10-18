import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersPrintersComponent } from './users-printers.component';

describe('UsersPrintersComponent', () => {
  let component: UsersPrintersComponent;
  let fixture: ComponentFixture<UsersPrintersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UsersPrintersComponent ]
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
