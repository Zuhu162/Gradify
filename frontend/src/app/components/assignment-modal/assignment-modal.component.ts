import {
  Component,
  ViewChild,
  ElementRef,
  Output,
  EventEmitter,
} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-assignment-modal',
  standalone: true,
  imports: [FormsModule], // ✅ Enable ngModel for form binding
  templateUrl: './assignment-modal.component.html',
  styleUrls: ['./assignment-modal.component.css'],
})
export class AssignmentModalComponent {
  @ViewChild('assignmentModal') assignmentModal!: ElementRef<HTMLDialogElement>;

  @Output() assignmentCreated = new EventEmitter<any>();

  assignmentData = { name: '', instructions: '', dueDate: '' };

  constructor(private httpClient: HttpClient) {}

  save() {
    const token = localStorage.getItem('authToken');

    if (!this.assignmentData.name || !this.assignmentData.dueDate) {
      alert('Please fill in all fields.');
      return;
    }

    // ✅ POST request to create a new assignment
    this.httpClient
      .post(
        `${environment.apiBaseUrl}/assignments/create`,
        this.assignmentData,
        {
          headers: { Authorization: `Bearer ${token}` },
        }
      )
      .subscribe(
        (newAssignment) => {
          this.assignmentCreated.emit(newAssignment);
          this.closeModal();
          setTimeout(() => {
            window.location.reload();
          }, 2000);
        },
        (error) => {
          console.error('Error creating assignment:', error);
        }
      );
  }

  openModal() {
    this.assignmentModal.nativeElement.showModal();
  }

  closeModal() {
    this.assignmentModal.nativeElement.close();
  }
}
