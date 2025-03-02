import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentsLoginComponent } from './student-login.component';

describe('StudentsLoginComponent', () => {
  let component: StudentsLoginComponent;
  let fixture: ComponentFixture<StudentsLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StudentsLoginComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(StudentsLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
