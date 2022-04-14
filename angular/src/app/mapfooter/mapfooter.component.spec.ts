import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MapfooterComponent } from './mapfooter.component';

describe('MapfooterComponent', () => {
  let component: MapfooterComponent;
  let fixture: ComponentFixture<MapfooterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MapfooterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MapfooterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
