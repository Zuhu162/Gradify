import { Injectable, inject } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class LoginGuard implements CanActivate {
  private router = inject(Router);

  canActivate(): boolean | UrlTree {
    const token = localStorage.getItem('authToken');

    if (token) {
      return this.router.createUrlTree(['/teacher-dashboard']);
    } else {
      return true;
    }
  }
}
