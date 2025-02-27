import { Injectable, inject } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class LoginGuard implements CanActivate {
  private router = inject(Router);

  private decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload)); // ✅ Decode JWT payload
    } catch (e) {
      console.error('Invalid token:', e);
      return null;
    }
  }

  canActivate(): boolean | UrlTree {
    const token = localStorage.getItem('authToken');

    if (token) {
      const decodedToken = this.decodeToken(token);
      const role = decodedToken?.role; // ✅ Get the role from the token

      if (role === 'Teacher') {
        return this.router.createUrlTree(['/teacher-dashboard']); // ✅ Redirect teachers
      } else if (role === 'Student') {
        return this.router.createUrlTree(['/student-dashboard']); // ✅ Redirect students
      }
    }

    return true;
  }
}
