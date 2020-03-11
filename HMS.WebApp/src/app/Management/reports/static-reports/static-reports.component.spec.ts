import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StaticReportsComponent } from './static-reports.component';

describe('StaticReportsComponent', () => {
  let component: StaticReportsComponent;
  let fixture: ComponentFixture<StaticReportsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StaticReportsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StaticReportsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
