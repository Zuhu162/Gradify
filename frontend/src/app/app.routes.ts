import { Routes } from '@angular/router';
import { TeacherLoginComponent } from './teacher-login/teacher-login.component';
import { TeacherDashboardComponent } from './teacher-dashboard/teacher-dashboard.component';

export const routes: Routes = [
  { path: '', redirectTo: 'teacher-login', pathMatch: 'full' },
  { path: 'teacher-login', component: TeacherLoginComponent },
  { path: 'teacher-dashboard', component: TeacherDashboardComponent },
];
