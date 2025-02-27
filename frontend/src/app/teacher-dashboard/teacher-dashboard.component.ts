import { HttpClient } from '@angular/common/http';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { environment } from '../../environments/environment';
import { DatePipe, NgClass, NgFor } from '@angular/common';

interface Assignments {
  id: String;
  name: String;
  instructions: String;
  dueData: Date;
  userId: String;
}

@Component({
  selector: 'app-teacher-dashboard',
  imports: [NgFor, DatePipe, NgClass],
  templateUrl: './teacher-dashboard.component.html',
  styleUrl: './teacher-dashboard.component.css',
  encapsulation: ViewEncapsulation.None, // Disable view encapsulation
})
export class TeacherDashboardComponent implements OnInit {
  assignments: any[] = [];

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
}
