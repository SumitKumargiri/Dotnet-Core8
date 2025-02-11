import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AttendancedataComponent } from './attendancedata.component';

describe('AttendancedataComponent', () => {
  let component: AttendancedataComponent;
  let fixture: ComponentFixture<AttendancedataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AttendancedataComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AttendancedataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
