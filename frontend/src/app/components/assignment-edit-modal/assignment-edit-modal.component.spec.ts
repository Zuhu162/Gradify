import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssignmentEditModalComponent } from './assignment-edit-modal.component';

describe('AssignmentEditModalComponent', () => {
  let component: AssignmentEditModalComponent;
  let fixture: ComponentFixture<AssignmentEditModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignmentEditModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignmentEditModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
