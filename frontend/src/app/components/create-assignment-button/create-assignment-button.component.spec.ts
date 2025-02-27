import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateAssignmentButtonComponent } from './create-assignment-button.component';

describe('CreateAssignmentButtonComponent', () => {
  let component: CreateAssignmentButtonComponent;
  let fixture: ComponentFixture<CreateAssignmentButtonComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CreateAssignmentButtonComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CreateAssignmentButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
