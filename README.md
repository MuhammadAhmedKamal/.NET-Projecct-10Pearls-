# .NET-Projecct-10Pearls-


Task Management System

A web-based application for seamless task management with user and admin functionalities.
Getting Started

Follow the steps below to set up the project locally for development and testing purposes.
Table of Contents

  -  Prerequisites
  -  Backend Setup
  -  Frontend Setup
  -  Tech Stack
  -  Additional Information

Prerequisites

Ensure your machine has the following installed:

  -  .NET SDK (version 6.0 or later)
  -  Node.js (version 14.0 or later)
  -  npm (Node Package Manager)
  -  SQL Server (or another preferred database)

Backend Setup
1. Clone the repository:

git clone [https://github.com/MuhammadAhmedKamal/]
cd [C#_Backend.zip]

2. Configure the database:

    {
      "ConnectionStrings": 
      {
        "DBConnection": "Server = DESKTOP-V6OJ7TG\\SQL2K22; Initial Catalog = DOTNET_DB; Persist Security Info=False; 
        User ID=sa; Password=Pak@123; 
        MultipleActiveResultSets=False; Encrypt=True; TrustServerCertificate=True;"
      }
    }

3. Run Entity Framework migrations:

dotnet ef migrations add InitialCreate
dotnet ef database update

4. Install dependencies:

dotnet restore

5. Run the backend server:

dotnet run/ Ctrl+f5 (in Visual Studio)

The backend API will be accessible at [https://localhost:7224/swagger/index.html.]
Frontend Setup
1. Navigate to the frontend directory:

cd [React_Frontend.zip]

2. Install dependencies:

npm install

3. Run the frontend:

npm start

The frontend will be available at [http://localhost:3000/]

Tech Stack

This project uses:

  -  React + javaScript (JSX) for the frontend
  -  ASP.NET Core Web API for the backend
  -  SQL Server Management Studio for the database
  -  Redux for state management in React
  -  Serilog for application logging 
  -  xUnit for unit testing 
  -  SonarQube for code quality analysis 

Additional Information

More features and implementation details will be added soon.

Let me know if you need further customization or additions!

    
