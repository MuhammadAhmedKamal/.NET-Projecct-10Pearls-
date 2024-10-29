# .NET-Projecct-10Pearls-


# Task Management Application

This is a task management application built with ASP.NET Core 8 for the backend and React for the frontend. It allows users to create, read, update, and delete tasks. The backend connects to a SQL Server database using Entity Framework Core.

---

## Features

- **Create** a new task with a title and description.
- **Read** all tasks from the database and display them in a table.
- **Update** existing tasks with new information.
- **Delete** tasks.
- **Frontend** built with React and styled using Bootstrap.
- **Backend** with ASP.NET Core 8 and Entity Framework Core for database interactions.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) and npm (for frontend)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or any SQL Server instance for the backend database
- [Visual Studio or VS Code](https://visualstudio.microsoft.com/) for development

---

## Getting Started

### Backend Setup (ASP.NET Core)

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/task-management-app.git
   cd task-management-app/Backend_website



## Project Structure

task-management-app/
│
├── Backend_website/                # ASP.NET Core backend
│   ├── Controllers/                # API Controllers
│   ├── Models/                     # Entity models and DbContext
│   └── Program.cs                  # Main configuration and startup
│
├── frontend/                       # React frontend
│   ├── src/
│   │   ├── components/             # React components
│   │   ├── pages/                  # Pages (e.g., Homepage.jsx)
│   │   └── App.js                  # Main app component
│   └── package.json                # npm dependencies
│
└── README.md                       # Project documentation



Common Issues

    CORS Errors: Ensure CORS is configured correctly in Program.cs to allow requests from http://localhost:3000.
    Database Connection Errors: Double-check the connection string in appsettings.json and ensure SQL Server is running.
    HTTPS Issues: If using https://localhost:7224, make sure your browser allows the self-signed certificate or switch to http for development.


##Contributing

Feel free to submit issues, fork the repository, and send pull requests. For major changes, please open an issue first to discuss what you would like to change.



    
