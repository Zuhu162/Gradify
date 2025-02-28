import {
  Component,
  ViewChild,
  ElementRef,
  Input,
  Output,
  EventEmitter,
} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-assignment-edit-modal',
  standalone: true,
  imports: [FormsModule], // ✅ Enable ngModel for form binding
  templateUrl: './assignment-edit-modal.component.html',
  styleUrls: ['./assignment-edit-modal.component.css'],
})
export class AssignmentEditModalComponent {
  @ViewChild('assignmentEditModal')
  assignmentEditModal!: ElementRef<HTMLDialogElement>;

  @Input() assignment: any = null; // ✅ Receive assignment to edit
  @Output() assignmentUpdated = new EventEmitter<any>();

  assignmentData = { id: 0, name: '', instructions: '', dueDate: '' };

  constructor(private httpClient: HttpClient) {}

  ngOnInit() {
    if (this.assignment) {
      this.assignmentData = { ...this.assignment };
    }
  }

  save() {
    const token = localStorage.getItem('authToken');

    if (!this.assignmentData.name || !this.assignmentData.dueDate) {
      alert('Please fill in all fields.');
      return;
    }

    // ✅ PATCH request to update an assignment
    this.httpClient
      .patch(
        `${environment.apiBaseUrl}/assignments/${this.assignmentData.id}/edit`,
        this.assignmentData,
        {
          headers: { Authorization: `Bearer ${token}` },
        }
      )
      .subscribe(
        (updatedAssignment) => {
          this.assignmentUpdated.emit(updatedAssignment);
          this.closeModal();
        },
        (error) => {
          console.error('Error updating assignment:', error);
        }
      );
  }

  openModal() {
    this.assignmentEditModal.nativeElement.showModal();
  }

  closeModal() {
    this.assignmentEditModal.nativeElement.close();
  }
}
