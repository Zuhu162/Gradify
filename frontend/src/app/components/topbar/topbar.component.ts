import { NgIf } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-topbar',
  standalone: true,
  templateUrl: './topbar.component.html',
  styleUrls: ['./topbar.component.css'],
  imports: [NgIf],
})
export class TopbarComponent implements OnInit {
  userName: string | null = null;

  constructor(private router: Router) {}

  ngOnInit(): void {
    const token = localStorage.getItem('authToken');

    if (token) {
      const decodedToken = this.decodeToken(token);
      this.userName = decodedToken?.unique_name || 'User';
    }
  }

  decodeToken(token: string): any {
    try {
      const payload = token.split('.')[1]; // Get the payload part
      return JSON.parse(atob(payload)); // Decode Base64
    } catch (e) {
      console.error('Invalid token:', e);
      return null;
    }
  }

  logout(): void {
    console.log('Logout function triggered'); // ✅ Debugging check
    localStorage.removeItem('authToken'); // ✅ Clear JWT token
    this.router.navigate(['/teacher-login']).then(() => {
      window.location.reload(); // ✅ Reload AFTER navigating to login
    });
  }
}
