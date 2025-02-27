import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-assignment-details',
  imports: [],
  templateUrl: './assignment-details.component.html',
  styleUrl: './assignment-details.component.css',
})
export class AssignmentDetailsComponent implements OnInit {
  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    const assignmentId = this.route.snapshot.paramMap.get('id');
    console.log('Assignment ID:', assignmentId); // ✅ Debugging log
  }
}
