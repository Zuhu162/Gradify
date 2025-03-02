# Assignment Grader

## Overview

**Gradify – A Teacher-Student Assignment Grading System** 📚✨

**Gradify** is a web-based platform that simplifies **assignment management and grading** for teachers and students. Teachers can **create assignments, track submissions, and grade efficiently**, while students can **submit their work via Google Drive or PDFs** and view their grades seamlessly. 🚀

## Architecture

Gradify follows a **Three-Tier Architecture** with a **RESTful API**:

- **Frontend (Angular 19)** – Handles UI/UX and communicates with the backend via REST APIs.
- **Backend (ASP.NET Core)** – Implements business logic, authentication, and API endpoints.
- **Database (SQL Server)** – Stores assignments, submissions, and user data securely.

This architecture ensures **scalability, security, and flexibility**, allowing future integrations with mobile apps or third-party services. 🔐🚀

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

### **Authentication**

| Login                                           | Registration                                                  |
| ----------------------------------------------- | ------------------------------------------------------------- |
| ![Login](frontend/public/screenshots/Login.JPG) | ![Registration](frontend/public/screenshots/Registration.JPG) |

### **Student Dashboard (Full Width)**

![Student Dashboard](frontend/public/screenshots/Student-Dashboard.JPG)

### **Student Individual Assignments**

| Unsubmitted                                                                                                 | Submitted                                                                                   | Graded                                                                                            |
| ----------------------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------- |
| <img src="frontend/public/screenshots/Student-Individual%20Assignment%20(Unsubmitted).JPG" width="400px" /> | <img src="frontend/public/screenshots/Student-Individual%20Assignment.JPG" width="400px" /> | <img src="frontend/public/screenshots/Student-Individual-Assignment(Graded).JPG" width="400px" /> |

### **Teacher Dashboard (Full Width)**

![Teacher Dashboard](frontend/public/screenshots/Teacher-Dashboard.JPG)

### **Teacher Individual Assignments**

| View Assignment                                                                             | Grade Assignment                                                                                  |
| ------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------- |
| <img src="frontend/public/screenshots/Teacher-Individual%20Assignment.JPG" width="400px" /> | <img src="frontend/public/screenshots/Teacher-Individual%20Assignment-Grade.JPG" width="400px" /> |

---

## Diagrams

### **Class Diagram**

![Class Diagram](backend/StudentGradeTracker/Diagrams/ClassDiagram.png)

### **System Architecture Diagram**

![System Architecture](backend/StudentGradeTracker/Diagrams/SystemArchitecture.png)

### **API Flow Diagram**

![API Flow](backend/StudentGradeTracker/Diagrams/APIflow.drawio.png)

---

## API Endpoints

### **Authentication**

- `POST /api/auth/register/{userType}` - Register a new user (Student/Teacher).
- `POST /api/auth/login/{userType}` - Login as a student or teacher.

### **User Management**

- `GET /api/users/students` - Get a list of all students (Teacher access only).

### **Assignments**

- `GET /api/assignments` - Fetch assignments created by the teacher.
- `POST /api/assignments/create` - Create a new assignment.
- `PATCH /api/assignments/{assignmentId}/edit` - Edit an existing assignment.
- `DELETE /api/assignments/{assignmentId}` - Delete an assignment.
- `PATCH /api/assignments/{assignmentId}/add-students` - Add students to an assignment.
- `PATCH /api/assignments/{assignmentId}/remove-student/{studentId}` - Remove a student from an assignment.
- `GET /api/assignments/{assignmentId}` - Fetch individual
