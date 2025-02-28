import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { environment } from '../../environments/environment';
import { DatePipe, NgClass, NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AssignmentModalComponent } from '../components/assignment-modal/assignment-modal.component';

interface Assignments {
  id: String;
  name: String;
  instructions: String;
  dueDate: string;
  userId: String;
  studentCount: number;
}

@Component({
  selector: 'app-teacher-dashboard',
  imports: [NgFor, DatePipe, NgClass, RouterLink, AssignmentModalComponent], // ✅ Import the modal component
  templateUrl: './teacher-dashboard.component.html',
  styleUrl: './teacher-dashboard.component.css',
  encapsulation: ViewEncapsulation.None, // Disable view encapsulation
})
export class TeacherDashboardComponent implements OnInit {
  assignments: Assignments[] = [];

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    const token = localStorage.getItem('authToken');

    this.httpClient
      .get<Assignments[]>(`${environment.apiBaseUrl}/assignments`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .subscribe((data) => {
        this.assignments = data;
        console.log(this.assignments);
      });
  }

  isDue(dueDate: string): boolean {
    return new Date(dueDate) < new Date();
  }

  @ViewChild('assignmentModal') assignmentModal!: AssignmentModalComponent;

  openCreateAssignmentModal() {
    this.assignmentModal.assignmentData = {
      name: '',
      instructions: '',
      dueDate: '',
    }; // ✅ Reset form
    this.assignmentModal.openModal();
  }
}
