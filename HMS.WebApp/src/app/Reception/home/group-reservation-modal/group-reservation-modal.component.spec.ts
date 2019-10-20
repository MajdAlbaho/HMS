import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GroupReservationModalComponent } from './group-reservation-modal.component';

describe('GroupReservationModalComponent', () => {
  let component: GroupReservationModalComponent;
  let fixture: ComponentFixture<GroupReservationModalComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GroupReservationModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GroupReservationModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
