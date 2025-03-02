import { Injectable, inject } from '@angular/core';
import {
  CanActivate,
  Router,
  ActivatedRouteSnapshot,
  UrlTree,
} from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private router = inject(Router);

  private decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload)); // ✅ Decode JWT manually
    } catch (e) {
      console.error('Invalid token:', e);
      return null;
    }
  }

  canActivate(route: ActivatedRouteSnapshot): boolean | UrlTree {
    const token = localStorage.getItem('authToken');

    if (!token) {
      return this.router.createUrlTree(['/login']); // ✅ Redirect if no token
    }

    const decodedToken = this.decodeToken(token);
    const userRole = decodedToken?.role; // ✅ Extract role from token
    const requiredRole = route.data['role']; // ✅ Get required role from route

    if (!userRole || userRole !== requiredRole) {
      return this.router.createUrlTree([
        userRole === 'Teacher' ? '/teacher-dashboard' : '/student-dashboard',
      ]);
    }

    return true;
  }
}
