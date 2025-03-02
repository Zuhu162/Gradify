# Assignment Grader

## Overview

**Assignment Grader** is a web-based system that allows **teachers** to create assignments, track student submissions, and grade assignments efficiently. **Students** can submit their work via Google Drive or PDF links and view their grades once assigned.

---

## Tech Stack

- **Frontend**: Angular 19, TailwindCSS v4
- **Backend**: C# ASP.NET Core
- **Database**: SQL Server
- **Authentication**: JWT-based authentication

---

## Features

### **For Students**

- View assignments with due dates.
- Submit assignment links (Google Drive/PDF).
- View grades once evaluated.

### **For Teachers**

- Create new assignments with instructions.
- View all student submissions.
- Grade submitted assignments.
- Manage enrolled students.

---

## Installation

### **Frontend Setup**

```sh
cd frontend
npm install
ng serve
```

Application will run on `http://localhost:4200`

### **Backend Setup**

```sh
cd backend
# Restore dependencies
dotnet restore
# Run the project
dotnet run
```

Backend runs on `http://localhost:5000`

---

## Screenshots

### **Authentication (Side-by-Side)**

| Login                                | Registration                                      |
| ------------------------------------ | ------------------------------------------------- |
| ![Login](frontend/public/Login.JPEG) | ![Registration](frontend/public/Registration.JPG) |

### **Student Dashboard (Full Width)**

![Student Dashboard](frontend/public/Student-Dashboard.JPG)

### **Student Individual Assignments (Side-by-Side)**

| Unsubmitted                                                                           | Submitted                                                         | Graded                                                                 |
| ------------------------------------------------------------------------------------- | ----------------------------------------------------------------- | ---------------------------------------------------------------------- |
| ![Unsubmitted](<frontend/public/Student-Individual%20Assignment%20(Unsubmitted).JPG>) | ![Submitted](frontend/public/Student-Individual%20Assignment.JPG) | ![Graded](<frontend/public/Student-Individual-Assignment(Graded).JPG>) |

### **Teacher Dashboard (Full Width)**

![Teacher Dashboard](frontend/public/Teacher-Dashboard.JPG)

### **Teacher Individual Assignments (Side-by-Side)**

| View Assignment                                              | Grade Assignment                                                    |
| ------------------------------------------------------------ | ------------------------------------------------------------------- |
| ![View](frontend/public/Teacher-Individual%20Assignment.JPG) | ![Grade](frontend/public/Teacher-Individual%20Assignment-Grade.JPG) |

---

## API Endpoints

### **Authentication**

- `POST /api/auth/login` - Login as a student or teacher.
- `POST /api/auth/register` - Register a new user.

### **Assignments**

- `GET /api/assignments` - Fetch assignments.
- `POST /api/assignments` - Create a new assignment.
- `PATCH /api/assignments/:id/grade` - Grade a student's submission.

### **Submissions**

- `POST /api/submissions` - Submit an assignment.
- `DELETE /api/submissions/:id` - Delete a submission.

---

## License

This project is licensed under the MIT License.

---

## Contact

For any issues or feature requests, feel free to reach out.
