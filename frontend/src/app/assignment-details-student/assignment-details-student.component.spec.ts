import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignmentDetailsStudentComponent } from './assignment-details-student.component';

describe('AssignmentDetailsStudentComponent', () => {
  let component: AssignmentDetailsStudentComponent;
  let fixture: ComponentFixture<AssignmentDetailsStudentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignmentDetailsStudentComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignmentDetailsStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
