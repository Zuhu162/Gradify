import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, RouterLink } from '@angular/router';
import { environment } from '../../environments/environment';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  imports: [FormsModule, RouterLink, NgIf],
})
export class RegisterComponent {
  userType: string = 'student'; // ✅ Default to Student
  name: string = '';
  email: string = '';
  password: string = '';
  successMessage: string = '';
  errorMessage: string = '';

  constructor(private httpClient: HttpClient, private router: Router) {}

  onRegister() {
    const registerData = {
      name: this.name,
      email: this.email,
      password: this.password,
    };

    this.httpClient
      .post(
        `${environment.apiBaseUrl}/auth/register/${this.userType}`,
        registerData,
        {
          responseType: 'text', // Expecting a success message
        }
      )
      .subscribe(
        (response) => {
          this.successMessage = response;
          setTimeout(() => {
            this.router.navigate([`/${this.userType}-login`]);
          }, 2000);
        },
        (error) => {
          console.error('Registration error:', error);
          this.errorMessage =
            error.status === 400
              ? error.error
              : 'An error occurred. Please try again later.';
        }
      );
  }
}
