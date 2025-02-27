import { Injectable, inject } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  private router = inject(Router);

  canActivate(): boolean | UrlTree {
    const token = localStorage.getItem('authToken');

    if (token) {
      return true;
    } else {
      return this.router.createUrlTree(['/teacher-login']);
    }
  }
}
