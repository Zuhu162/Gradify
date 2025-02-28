import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../environments/environment';
import { forkJoin } from 'rxjs';
import { DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-assignment-details',
  imports: [NgFor, FormsModule, DatePipe, NgIf, NgClass],
  standalone: true,
  templateUrl: './assignment-details.component.html',
  styleUrl: './assignment-details.component.css',
})
export class AssignmentDetailsComponent implements OnInit {
  id: string | null = null;
  submissions: any = {};
  assignment: any = {};
  authToken: any = '';

  constructor(private route: ActivatedRoute, private httpClient: HttpClient) {}

  isGraded(status: string): boolean {
    return status === 'Graded';
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    const token = localStorage.getItem('authToken');
    this.authToken = token;

    if (this.id) {
      forkJoin({
        assignment: this.httpClient.get(
          `${environment.apiBaseUrl}/assignments/${this.id}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        ),
        submissions: this.httpClient.get(
          `${environment.apiBaseUrl}/submissions/${this.id}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        ),
      }).subscribe({
        next: (results) => {
          this.assignment = results.assignment;
          this.submissions = results.submissions;
          console.log('Assignments', this.assignment);
          console.log('Submissions', this.submissions);
        },
        error: (err) => {
          console.error('Error fetching data:', err);
        },
      });
    }
  }

  //Grading Modal
  selectedSubmission: any = null;
  selectedGrade: string = '';

  @ViewChild('gradeModal', { static: false })
  gradeModal!: ElementRef<HTMLDialogElement>;
  openGradeModal(submission: any) {
    this.selectedSubmission = submission;
    this.gradeModal.nativeElement.showModal(); // Open modal
  }

  closeGradeModal() {
    if (this.gradeModal?.nativeElement) {
      this.gradeModal.nativeElement.close();
    }
    this.selectedSubmission = null;
    this.selectedGrade = '';
  }

  saveGrade() {
    if (!this.selectedSubmission || !this.selectedGrade) return;

    const submissionId = this.selectedSubmission.id;

    this.httpClient
      .patch(
        `${environment.apiBaseUrl}/submissions/${submissionId}/grade`,
        { grade: this.selectedGrade },
        {
          headers: { Authorization: `Bearer ${this.authToken}` },
          responseType: 'text',
        }
      )
      .subscribe(
        () => {
          this.selectedSubmission.grade = this.selectedGrade;
          this.closeGradeModal();
        },
        (error) => {
          console.error('Error updating grade:', error);
        }
      );
  }

  //Add Students Modal
  @ViewChild('addStudentsModal', { static: false })
  addStudentsModal!: ElementRef<HTMLDialogElement>;

  studentIdToAdd: string = ''; // ✅ Store entered Student ID

  openAddStudentsModal() {
    this.addStudentsModal.nativeElement.showModal(); // ✅ Open modal
  }

  closeAddStudentsModal() {
    if (this.addStudentsModal?.nativeElement) {
      this.addStudentsModal.nativeElement.close();
    }
    this.studentIdToAdd = ''; // ✅ Reset input field
  }

  saveStudent() {
    if (!this.studentIdToAdd.trim()) return;

    const studentIds = [parseInt(this.studentIdToAdd)];

    this.httpClient
      .patch(
        `${environment.apiBaseUrl}/assignments/${this.assignment.id}/add-students`,
        studentIds,
        {
          headers: { Authorization: `Bearer ${this.authToken}` },
          responseType: 'text',
        }
      )
      .subscribe(
        () => {
          this.showToast('Student added successfully!', 'success'); // ✅ Show success toast
          this.closeAddStudentsModal();
          setTimeout(() => {
            window.location.reload();
          }, 3000);
        },
        (error) => {
          console.error('Error adding student:', error);

          if (error.status === 400) {
            this.showToast(
              'No valid student found. Please enter a correct Student ID.',
              'error'
            );
          } else if (error.status === 404) {
            this.showToast(
              'Assignment not found. Please refresh and try again.',
              'error'
            );
          } else {
            this.showToast(
              `Failed to add student: ${error.error || error.message}`,
              'error'
            );
          }
        }
      );
  }

  //Remove Students Modal
  confirmRemoveStudent(studentId: number) {
    const confirmed = confirm('Are you sure you want to remove this student?');
    if (!confirmed) return;

    this.httpClient
      .patch(
        `${environment.apiBaseUrl}/assignments/${this.assignment.id}/remove-student/${studentId}`,
        {}, // ✅ Empty body
        {
          headers: { Authorization: `Bearer ${this.authToken}` },
          responseType: 'text',
        }
      )
      .subscribe(
        () => {
          // Remove student from studentAssignments array
          this.assignment.studentAssignments =
            this.assignment.studentAssignments.filter(
              (s: any) => s.userId !== studentId
            );
          this.assignment.studentCount--; // Update student count dynamically
          this.showToast('Student Removed Successfully', 'success');
        },
        (error) => {
          console.error('Error removing student:', error);
          this.showToast('Error while removing student', 'error');
        }
      );
  }

  toastMessage: string = '';
  toastType: string = '';
  //Toast
  showToast(message: string, type: string) {
    this.toastMessage = message;
    this.toastType =
      type === 'success' ? 'alert alert-success' : 'alert alert-error';

    setTimeout(() => {
      this.toastMessage = ''; // ✅ Auto-hide toast after 3 seconds
    }, 3000);
  }
}
