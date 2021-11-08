import {Component, NgModule, OnDestroy, OnInit} from '@angular/core';
import {ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR} from '@angular/forms';
import {Manufacturer} from '../../../../core/models/manufacturer';
import {ManufacturersService} from '../../../../core/services/manufacturers.service';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {BehaviorSubject, Subscription} from 'rxjs';
import {debounceTime, distinctUntilChanged, switchMap} from 'rxjs/operators';
import {CommonModule} from '@angular/common';
import {IPaginated} from '../../../../core/models/ipaginated';
import {Paginated} from '../../../../core/models/paginated';
import {MatIconModule} from '@angular/material/icon';
import {MatButtonModule} from '@angular/material/button';
import {MatDividerModule} from '@angular/material/divider';

@Component({
  selector: 'app-manufacturer-visual-selector',
  templateUrl: './manufacturer-visual-selector.component.html',
  styleUrls: ['./manufacturer-visual-selector.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: ManufacturerVisualSelectorComponent
    }
  ]
})
export class ManufacturerVisualSelectorComponent implements OnInit, OnDestroy, ControlValueAccessor {
  get manufacturerName(): string {
    return this._manufacturerName.value;
  }

  set manufacturerName(value: string) {
    this._manufacturerName.next(value);
  }

  constructor(private _manufacturersService: ManufacturersService) { }

  manufacturer: Manufacturer;

  touched = false;
  disabled = false;

  private _manufacturerName: BehaviorSubject<string> = new BehaviorSubject<string>('');
  private _nameSearchSubscription: Subscription;
  searchResults: IPaginated<Manufacturer> = new Paginated([], 1, 0, 25);

  onTouched: any = () => {};
  onChange: any = (manufacturer: Manufacturer) => {};

  ngOnInit(): void {
    this._nameSearchSubscription = this._manufacturerName
      .pipe(
        debounceTime(500),
        distinctUntilChanged(),
        switchMap(name => this._manufacturersService.getList(name))
      )
      .subscribe(result => {
        this.searchResults = result;
      });
  }

  ngOnDestroy(): void {
    this._nameSearchSubscription.unsubscribe();
    this._manufacturerName.complete();
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  writeValue(obj: any): void {
    this.manufacturer = obj;
  }

}

@NgModule({
  imports: [
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    CommonModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule
  ],
  exports: [
    ManufacturerVisualSelectorComponent
  ],
  declarations: [ManufacturerVisualSelectorComponent]
})
export class ManufacturerVisualSelectorModule {}
