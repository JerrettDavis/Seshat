import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUserPrinterComponent } from './create-user-printer.component';

describe('CreateUserPrinterComponent', () => {
  let component: CreateUserPrinterComponent;
  let fixture: ComponentFixture<CreateUserPrinterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateUserPrinterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateUserPrinterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
