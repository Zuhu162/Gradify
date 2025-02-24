import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; // ✅ Import FormsModule for two-way data binding

@Component({
  selector: 'app-teacher-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './teacher-login.component.html',
  styleUrls: ['./teacher-login.component.css'],
})
export class TeacherLoginComponent {
  email: string = '';
  password: string = '';

  // ✅ Handle form submission
  onSubmit(): void {
    if (this.email && this.password) {
      console.log('Form Submitted!');
      console.log('Email:', this.email);
      console.log('Password:', this.password);
    } else {
      console.log('Form is invalid.');
    }
  }
}
