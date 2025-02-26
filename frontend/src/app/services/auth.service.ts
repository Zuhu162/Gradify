import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = environment.apiBaseUrl;

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'An unknown error occurred!';

    if (error.error instanceof ErrorEvent) {
      // ✅ Client-side or network error
      errorMessage = `Client-side error: ${error.error.message}`;
    } else {
      // ✅ Server-side error based on status code
      switch (error.status) {
        case 400:
          errorMessage = 'Bad Request. Please check your input.';
          break;
        case 401:
          errorMessage = 'Invalid credentials. Please try again.'; // ✅ Custom message for unauthorized
          break;
        case 403:
          errorMessage = 'You do not have permission to access this resource.';
          break;
        case 500:
          errorMessage = 'Server error. Please try again later.';
          break;
        default:
          errorMessage = `Unexpected error: ${error.status} - ${error.statusText}`;
          break;
      }
    }

    return throwError(() => new Error(errorMessage));
  }

  constructor(private http: HttpClient) {}

  loginTeacher(email: string, password: string): Observable<any> {
    const loginData = { email, password };
    return this.http
      .post<any>(`${this.apiUrl}/auth/login/teacher`, loginData)
      .pipe(
        tap((response) => {
          if (response && response.token) {
            localStorage.setItem('authToken', response.token);
          }
        }),
        catchError(this.handleError)
      );
  }

  // Retrieve the stored JWT token
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // Logout and remove the JWT token
  logout(): void {
    localStorage.removeItem('authToken');
  }
}
