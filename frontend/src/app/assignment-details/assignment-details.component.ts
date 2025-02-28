import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from '../../environments/environment';
import { forkJoin } from 'rxjs';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-assignment-details',
  imports: [NgFor],
  standalone: true,
  templateUrl: './assignment-details.component.html',
  styleUrl: './assignment-details.component.css',
})
export class AssignmentDetailsComponent implements OnInit {
  id: string | null = null;
  submissions: any = {};
  assignment: any = {};

  constructor(private route: ActivatedRoute, private httpClient: HttpClient) {}

  isGraded(status: string): boolean {
    return status === 'Graded';
  }

  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id');
    const token = localStorage.getItem('authToken');

    if (this.id) {
      forkJoin({
        assignment: this.httpClient.get(
          `${environment.apiBaseUrl}/assignments/${this.id}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        ),
        submissions: this.httpClient.get(
          `${environment.apiBaseUrl}/submissions/${this.id}`,
          {
            headers: { Authorization: `Bearer ${token}` },
          }
        ),
      }).subscribe({
        next: (results) => {
          this.assignment = results.assignment;
          this.submissions = results.submissions;
          console.log('Assignments', this.assignment);
          console.log('Submissions', this.submissions);
        },
        error: (err) => {
          console.error('Error fetching data:', err);
        },
      });
    }
  }
}
