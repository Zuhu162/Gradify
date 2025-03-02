import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { DatePipe, NgClass, NgFor } from '@angular/common';

@Component({
  selector: 'app-student-dashboard',
  standalone: true,
  imports: [DatePipe, NgFor, NgClass],
  templateUrl: './student-dashboard.component.html',
  styleUrls: ['./student-dashboard.component.css'],
})
export class StudentDashboardComponent implements OnInit {
  assignments: any[] = [];
  authToken: string | null = '';

  constructor(private httpClient: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.authToken = localStorage.getItem('authToken');

    // ✅ Fetch assignments assigned to the student
    this.httpClient
      .get<any[]>(`${environment.apiBaseUrl}/assignments/student-assignments`, {
        headers: { Authorization: `Bearer ${this.authToken}` },
      })
      .subscribe(
        (data) => {
          this.assignments = data;
        },
        (error) => {
          console.error('Error fetching assignments:', error);
        }
      );
  }

  isDue(dueDate: string): boolean {
    return new Date(dueDate) < new Date();
  }

  // ✅ Navigate to assignment details page
  goToAssignmentDetails(assignmentId: number) {
    this.router.navigate([`/student-assignment/${assignmentId}`]);
  }
}
