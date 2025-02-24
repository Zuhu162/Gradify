import { Routes } from '@angular/router';
import { TeacherLoginComponent } from './teacher-login/teacher-login.component';

export const routes: Routes = [
  { path: '', component: TeacherLoginComponent },
  { path: 'teacher-login', component: TeacherLoginComponent },
];
