import { TestBed } from '@angular/core/testing';

import { AppDatabaseService } from './app-database.service';

describe('AppDatabaseService', () => {
  let service: AppDatabaseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppDatabaseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should have seed data', async () => {
    const manufacturers = await service.manufacturers.toArray();
    const printers = await service.printers.toArray();

    expect(manufacturers).toBeTruthy();
    expect(manufacturers.length).toBeGreaterThan(0);
    expect(printers).toBeTruthy();
    expect(printers.length).toBeGreaterThan(0)
  });

  it('should populate printer manufacturer', async () => {
    const printer = (await service.printers.toCollection().first())
      .makeReady();

    await printer.manufacturer.load(service);

    const manufacturer = printer.manufacturer;
    expect(manufacturer.value).toBeTruthy();
    expect(manufacturer.value.name).toBeTruthy();
  })

  afterEach(async () => {
    await service.delete();
  });
});
