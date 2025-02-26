import { Component } from '@angular/core';

interface Assignments {
  id: String;
  name: String;
  instructions: String;
  dueData: Date;
  userId: String;
}

@Component({
  selector: 'app-teacher-dashboard',
  imports: [],
  templateUrl: './teacher-dashboard.component.html',
  styleUrl: './teacher-dashboard.component.css',
})
export class TeacherDashboardComponent {
  assignments: Assignments[] = [];
}
