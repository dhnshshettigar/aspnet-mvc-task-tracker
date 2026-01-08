# Task Tracker – ASP.NET MVC

A simple **Task Tracker web application** built using **ASP.NET MVC (non-Core)** and **ADO.NET**, focused on understanding backend–database interaction, authentication, and CRUD operations without using Entity Framework.

This project was built as a **hands-on learning application** to understand how classic ASP.NET MVC applications work internally.

---

## 🚀 Features

### Authentication
- User Registration with validation
- User Login
- Session-based authentication
- Logout functionality

### Task Management
- Create tasks
- View tasks (user-specific)
- Edit tasks
- Mark tasks as completed / pending
- Delete tasks
- Tasks are visible **only to the logged-in user**

---

## 🛠️ Tech Stack

- **ASP.NET MVC 5 (.NET Framework)**
- **ADO.NET** (SqlConnection, SqlCommand, SqlDataReader)
- **SQL Server** (SSMS)
- **Razor Views**
- **Session-based Authentication**

> ❌ No Entity Framework  
> ❌ No ASP.NET Identity  
> ✅ Manual SQL & authentication for better understanding

---

## 📂 Project Structure

```

TaskTracker
│
├── Controllers
│   ├── AccountController.cs
│   └── TaskController.cs
│
├── Models
│   └── TaskItem.cs
│
├── DAL
│   └── DbHelper.cs
│
├── Views
│   ├── Account
│   │   ├── Login.cshtml
│   │   └── Register.cshtml
│   └── Task
│       ├── Index.cshtml
│       ├── Create.cshtml
│       └── Edit.cshtml
│
├── Web.config
└── Global.asax

```

---

## 🗄️ Database Design

### Users Table
| Column | Type |
|------|------|
| UserId | INT (PK) |
| UserName | NVARCHAR(50) |
| Email | NVARCHAR(100) |
| PasswordHash | NVARCHAR(255) |

### Tasks Table
| Column | Type |
|------|------|
| TaskId | INT (PK) |
| Title | NVARCHAR(100) |
| Description | NVARCHAR(255) |
| IsComplete | BIT |
| UserId | INT (FK) |

---

## 🔐 Authentication Flow

1. User registers
2. Credentials stored in database
3. User logs in
4. UserId stored in Session
5. All task operations are filtered using Session UserId
6. Logout clears session

---

## ✅ What I Learned

- ASP.NET MVC request lifecycle
- Form submission and model binding
- Session handling
- SQL injection prevention using parameters
- Manual CRUD operations using ADO.NET
- Importance of database constraints
- Handling common MVC issues (checkbox binding, validation, routing ambiguity)

---

## ⚠️ Notes

- Passwords are currently stored in plain text (for learning purposes).
- Can be enhanced by adding password hashing, filters, and UI improvements.

---

## 📌 Future Improvements

- Password hashing
- Authorization filter instead of manual session checks
- UI enhancement using Bootstrap
- Task filtering (Completed / Pending)

---

## 👤 Author

Built by **Dhanush Shettigar**  
```
