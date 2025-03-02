import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-students-list',
  standalone: true,
  templateUrl: './students-list.component.html',
  styleUrls: ['./students-list.component.css'],
  imports: [NgFor, RouterLink],
})
export class StudentsListComponent implements OnInit {
  students: any[] = [];
  authToken: string | null = '';

  constructor(private httpClient: HttpClient) {}

  ngOnInit(): void {
    this.authToken = localStorage.getItem('authToken');

    this.httpClient
      .get<any[]>(`${environment.apiBaseUrl}/users/students`, {
        headers: { Authorization: `Bearer ${this.authToken}` },
      })
      .subscribe(
        (data) => {
          this.students = data;
        },
        (error) => {
          console.error('Error fetching students:', error);
        }
      );
  }
}
