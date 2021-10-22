import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUserPrinterComponent } from './create-user-printer.component';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatStepperModule} from '@angular/material/stepper';
import {ReactiveFormsModule} from '@angular/forms';
import {NoopAnimationsModule} from '@angular/platform-browser/animations';

describe('CreateUserPrinterComponent', () => {
  let component: CreateUserPrinterComponent;
  let fixture: ComponentFixture<CreateUserPrinterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateUserPrinterComponent ],
      imports: [
        MatButtonModule,
        MatFormFieldModule,
        MatInputModule,
        MatStepperModule,
        NoopAnimationsModule,
        ReactiveFormsModule
      ]
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
