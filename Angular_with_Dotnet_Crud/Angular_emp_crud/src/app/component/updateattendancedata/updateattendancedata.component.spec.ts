import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateattendancedataComponent } from './updateattendancedata.component';

describe('UpdateattendancedataComponent', () => {
  let component: UpdateattendancedataComponent;
  let fixture: ComponentFixture<UpdateattendancedataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [UpdateattendancedataComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(UpdateattendancedataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
