import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { DatePipe, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-assignment-details-student',
  standalone: true,
  templateUrl: './assignment-details-student.component.html',
  imports: [DatePipe, NgIf, FormsModule],
  styleUrls: ['./assignment-details-student.component.css'],
})
export class AssignmentDetailsStudentComponent implements OnInit {
  assignment: any = {};
  submission: any = null;
  authToken: string | null = '';
  submissionLink: string = '';

  constructor(
    private route: ActivatedRoute,
    private httpClient: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.authToken = localStorage.getItem('authToken');
    const assignmentId = this.route.snapshot.paramMap.get('id');

    if (assignmentId) {
      // ✅ Fetch assignment details
      this.httpClient
        .get(
          `${environment.apiBaseUrl}/assignments/${assignmentId}/student-view`,
          {
            headers: { Authorization: `Bearer ${this.authToken}` },
          }
        )
        .subscribe(
          (data) => {
            this.assignment = data;
          },
          (error) => {
            console.error('Error fetching assignment:', error);
          }
        );

      // ✅ Fetch submission status
      this.httpClient
        .get(
          `${environment.apiBaseUrl}/submissions/${assignmentId}/my-submission`,
          {
            headers: { Authorization: `Bearer ${this.authToken}` },
          }
        )
        .subscribe(
          (data) => {
            this.submission = data;
          },
          () => {
            this.submission = null; // No submission found
          }
        );
    }
  }

  // Submit assignment
  submitAssignment() {
    if (!this.submissionLink.trim()) {
      alert('Please enter a valid submission link.');
      return;
    }

    const submissionData = {
      assignmentId: this.assignment.id,
      fileUrl: this.submissionLink,
    };

    this.httpClient
      .post(`${environment.apiBaseUrl}/submissions/submit`, submissionData, {
        headers: { Authorization: `Bearer ${this.authToken}` },
      })
      .subscribe(
        () => {
          alert('Assignment submitted successfully!');
          this.submission = { fileUrl: this.submissionLink, grade: 'Ungraded' }; // Update UI
        },
        (error) => {
          console.error('Error submitting assignment:', error);
          alert('Submission failed. Please try again.');
        }
      );
  }

  // Delete submission
  deleteSubmission() {
    if (!this.submission?.id) return;
    const confirmed = confirm(
      'Are you sure you want to delete this submission?'
    );

    if (!confirmed) return;

    this.httpClient
      .delete(
        `${environment.apiBaseUrl}/submissions/${this.submission.id}/delete`,
        {
          headers: { Authorization: `Bearer ${this.authToken}` },
        }
      )
      .subscribe(
        () => {
          alert('Submission deleted successfully!');
          this.submission = null; // Update UI
        },
        (error) => {
          console.error('Error deleting submission:', error);
          alert('Error deleting submission.');
        }
      );
  }
}
