import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManufacturerVisualSelectorComponent } from './manufacturer-visual-selector.component';

describe('ManufacturerVisualSelectorComponent', () => {
  let component: ManufacturerVisualSelectorComponent;
  let fixture: ComponentFixture<ManufacturerVisualSelectorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManufacturerVisualSelectorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManufacturerVisualSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
