# Company Management System

A web-based management system built with ASP.NET Core MVC that helps organizations manage employees, departments, user accounts, and role assignments through centralized administrative dashboards.

## Role

**Full-Stack Developer**

## Technologies Used

* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* ASP.NET Identity
* AutoMapper
* Bootstrap
* SMTP Email Service

## Features

* Employee, Department, User & Role Management
* Authentication & Authorization
* Password Recovery & Reset via Email
* File Upload Service
* Search & Filtering Capabilities
* CRUD Operations

## Software Architecture

This project follows an **N-Tier Architecture** approach to separate responsibilities across different layers:

### Presentation Layer

Responsible for handling user requests and rendering views.

### Business Logic Layer

Contains application services and business rules.

### Data Access Layer

Responsible for database interaction through repositories and Entity Framework Core.

## Design Patterns & Concepts

During this project, I applied several software engineering concepts:

* Repository Pattern
* Unit of Work Pattern
* Dependency Injection
* Service Layer Pattern
* DTOs (Data Transfer Objects)
* ViewModels
* AutoMapper
* ASP.NET Identity
* Entity Framework Core (Code First)
* Data Annotations
* Fluent API

## Database

The application uses SQL Server with the Entity Framework Core Code First approach.

Key concepts implemented:

* Database Relationships
* Migrations
* ERD Design
* Database Schema Planning
* Fluent API Configurations

## Screenshots

### Home Page

![Home Page](screenshots/HomePage.png)

### Login Page

![Login Page](screenshots/LoginPage.png)

### Register Page

![Register Page](screenshots/RegisterPage.png)

### Employee Management

![Employee Management](screenshots/EmployeeManagement.png)

### Department Management

![Department Management](screenshots/DepartmentManagement.png)

### User Management

![User Management](screenshots/UserManagement.png)

### Role Management

![Role Management](screenshots/RoleManagement.png)

## What I Learned

This project marked a significant evolution in my backend engineering journey.

Through building this application, I learned:

* Structured software architecture using N-Tier Architecture
* Separation of concerns across application layers
* Clean code principles and reducing code duplication
* Database design and planning before implementation
* Authentication and authorization using ASP.NET Identity
* Managing data flow using DTOs, ViewModels, and AutoMapper
* Building maintainable and scalable applications

## Getting Started

### Prerequisites

* .NET 8 SDK
* SQL Server
* Visual Studio 2022

### Installation

1. Clone the repository.
2. Configure the SQL Server connection string in `appsettings.json`.
3. Apply database migrations.
4. Run the application.

## Future Improvements

* Full Async/Await Implementation
* API Version of the Application
* Audit Logging
* Advanced Reporting Dashboard
* Unit Testing

## Author

**Mohamed Walid Abdullah**
