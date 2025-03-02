import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';
import { environment } from '../../environments/environment';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-student-login',
  standalone: true,
  templateUrl: './student-login.component.html',
  styleUrls: ['./student-login.component.css'],
  imports: [FormsModule, NgIf, RouterLink],
})
export class StudentLoginComponent {
  email: string = '';
  password: string = '';
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private router: Router, private authService: AuthService) {}

  onSubmit(): void {
    if (this.email && this.password) {
      this.authService.loginStudent(this.email, this.password).subscribe({
        next: (response) => {
          this.successMessage = 'Login successful!';
          this.errorMessage = '';
          // Redirect to dashboard after successful login
          this.router
            .navigate(['/student-dashboard'])
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
