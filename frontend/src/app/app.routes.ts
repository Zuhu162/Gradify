import { Routes } from '@angular/router';
import { TeacherLoginComponent } from './teacher-login/teacher-login.component';
import { TeacherDashboardComponent } from './teacher-dashboard/teacher-dashboard.component';
import { AuthGuard } from './guards/auth.guard';
import { AssignmentDetailsComponent } from './assignment-details/assignment-details.component';
import { LoginGuard } from './guards/login.guard';
import { StudentsListComponent } from './students-list/students-list.component';
import { StudentLoginComponent } from './students-login/student-login.component';
import { RegisterComponent } from './register/register.component';
import { StudentDashboardComponent } from './student-dashboard/student-dashboard.component';
import { AssignmentDetailsStudentComponent } from './assignment-details-student/assignment-details-student.component';

export const routes: Routes = [
  { path: '', redirectTo: 'teacher-login', pathMatch: 'full' },
  {
    path: 'teacher-login',
    component: TeacherLoginComponent,
    canActivate: [LoginGuard],
  },
  {
    path: 'student-login',
    component: StudentLoginComponent,
    // canActivate: [LoginGuard],
  },
  {
    path: 'teacher-dashboard',
    component: TeacherDashboardComponent,
    canActivate: [AuthGuard],
    data: { role: 'Teacher' },
  },
  {
    path: 'student-dashboard',
    component: StudentDashboardComponent,
    canActivate: [AuthGuard],
    data: { role: 'Student' },
  },
  {
    path: 'assignment/:id',
    component: AssignmentDetailsComponent,
  },
  {
    path: 'student-assignment/:id',
    component: AssignmentDetailsStudentComponent,
  },
  { path: 'students', component: StudentsListComponent },
  { path: 'register', component: RegisterComponent },
];
