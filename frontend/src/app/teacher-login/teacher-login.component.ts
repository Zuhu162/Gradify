import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-teacher-login',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './teacher-login.component.html',
  styleUrls: ['./teacher-login.component.css'],
})
export class TeacherLoginComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  // form submission
  onSubmit(): void {
    if (this.email && this.password) {
      this.authService.loginTeacher(this.email, this.password).subscribe({
        next: (response) => {
          this.successMessage = 'Login successful!';
          this.errorMessage = '';
          // Redirect to dashboard after successful login
          this.router
            .navigate(['/teacher-dashboard'])
            .then(() => window.location.reload()); // Replace with the actual route
        },
        error: (error) => {
          this.errorMessage = error.message;
          this.successMessage = '';
        },
      });
    } else {
      this.errorMessage = 'Please enter both email and password.';
    }
  }
}
