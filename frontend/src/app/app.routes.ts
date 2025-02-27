import { Routes } from '@angular/router';
import { TeacherLoginComponent } from './teacher-login/teacher-login.component';
import { TeacherDashboardComponent } from './teacher-dashboard/teacher-dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { AssignmentDetailsComponent } from './assignment-details/assignment-details.component';

export const routes: Routes = [
  { path: '', redirectTo: 'teacher-login', pathMatch: 'full' },
  {
    path: 'teacher-login',
    component: TeacherLoginComponent,
  },
  {
    path: 'teacher-dashboard',
    component: TeacherDashboardComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'assignment/:id',
    component: AssignmentDetailsComponent,
  },
];
