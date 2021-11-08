import {Component, NgModule, OnInit} from '@angular/core';
import {MatStepperModule} from '@angular/material/stepper';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatButtonModule} from '@angular/material/button';
import {MatCardModule} from '@angular/material/card';
import {ManufacturerVisualSelectorModule} from '../../../manufacturers/components/manufacturer-visual-selector/manufacturer-visual-selector.component';

@Component({
  selector: 'app-create-user-printer',
  templateUrl: './create-user-printer.component.html',
  styleUrls: ['./create-user-printer.component.scss']
})
export class CreateUserPrinterComponent implements OnInit {
  firstFormGroup: FormGroup;
  secondFormGroup: FormGroup;

  constructor(private _formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.firstFormGroup = this._formBuilder.group({
      firstCtrl: ['', Validators.required]
    });
    this.secondFormGroup = this._formBuilder.group({
      secondCtrl: ['', Validators.required]
    });
  }

}

@NgModule({
  imports: [
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatStepperModule,
    ReactiveFormsModule,
    MatCardModule,
    ManufacturerVisualSelectorModule
  ],
  declarations: [CreateUserPrinterComponent]
})
export class CreateUserPrinterPageModule {}
