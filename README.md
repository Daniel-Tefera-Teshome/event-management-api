# Event Management API

A RESTful Event Management API built with **ASP.NET Core Web API**, **Entity Framework Core**, and **PostgreSQL**.

This project is being developed as a practical C#/.NET learning project, with the goal of progressing from a basic CRUD API toward a production-style backend architecture.

## 🚀 Tech Stack

* **C#**
* **.NET 10**
* **ASP.NET Core Web API**
* **Entity Framework Core**
* **PostgreSQL**
* **Npgsql**
* **Swagger / OpenAPI**
* **REST API**
* **Dependency Injection**
* **DTOs**
* **Git / GitHub**

## 📁 Project Structure

```text
EventManagement/
│
├── EventManagement.Api/
│   ├── Controllers/
│   │   └── EventsController.cs
│   │
│   ├── Data/
│   │   └── EventDbContext.cs
│   │
│   ├── Models/
│   │   └── Event.cs
│   │
│   ├── Services/
│   │   ├── IEventService.cs
│   │   └── EventService.cs
│   │
│   ├── Program.cs
│   └── appsettings.json
│
├── EventManagement.DTOs/
│   └── Events/
│       ├── EventDto.cs
│       ├── CreateEventDto.cs
│       └── UpdateEventDto.cs
│
├── .gitignore
├── README.md
└── EventManagement.sln
```

## ✨ Current Features

### Event Management

The API currently supports complete CRUD operations for events:

| Method   | Endpoint           | Description              |
| -------- | ------------------ | ------------------------ |
| `GET`    | `/api/events`      | Get all events           |
| `GET`    | `/api/events/{id}` | Get an event by ID       |
| `POST`   | `/api/events`      | Create a new event       |
| `PUT`    | `/api/events/{id}` | Update an existing event |
| `DELETE` | `/api/events/{id}` | Delete an event          |

### Event Data

An event currently contains:

* ID
* Name
* Description
* Start Date
* End Date
* Location

## 🏗️ Architecture

The project is being developed using a layered approach.

```text
Client
   │
   ▼
EventsController
   │
   ▼
IEventService
   │
   ▼
EventService
   │
   ▼
EventDbContext
   │
   ▼
PostgreSQL
```

### Controller

The controller handles HTTP requests and HTTP responses.

```text
HTTP Request
     ↓
Controller
     ↓
Service
     ↓
HTTP Response
```

### Service Layer

The service layer contains application and business logic and keeps database-related operations out of the controller.

For example:

```csharp
var events = await _eventService.GetEventsAsync();
```

### Entity Framework Core

EF Core is used as the ORM for communication between the application and PostgreSQL.

```text
C# Entity
   ↓
Entity Framework Core
   ↓
PostgreSQL
```

### DTOs

Data Transfer Objects are used to control the data exchanged through the API.

Current DTOs include:

* `EventDto`
* `CreateEventDto`
* `UpdateEventDto`

## 🗄️ Database

The application uses **PostgreSQL** as its database.

Entity Framework Core is used for:

* Database connection
* Querying
* Creating records
* Updating records
* Deleting records
* Database migrations

The database configuration is kept outside the source code.

### Environment Configuration

Sensitive configuration is stored in a local `.env` file.

Example:

```env
ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=event_management;Username=postgres;Password=your_password
```

The `.env` file is intentionally excluded from Git using `.gitignore`.

> **Never commit real database passwords, API keys, tokens, or other secrets to GitHub.**

## ▶️ Getting Started

### Prerequisites

Make sure you have installed:

* .NET 10 SDK
* PostgreSQL
* Git

### Clone the repository

```bash
git clone https://github.com/Daniel-Tefera-Teshome/event-management-api.git
cd event-management-api
```

### Configure the database

Create a local `.env` file in the project root:

```env
ConnectionStrings__DefaultConnection=Host=localhost;Port=5432;Database=event_management;Username=postgres;Password=your_password
```

Replace the database credentials with your local PostgreSQL configuration.

### Restore dependencies

```bash
dotnet restore
```

### Apply database migrations

```bash
dotnet ef database update
```

### Run the application

```bash
dotnet run
```

The API will start on the configured local HTTP/HTTPS ports.

## 📖 API Documentation

Swagger / OpenAPI is enabled in the development environment.

After starting the application, open:

```text
/swagger
```

Swagger provides an interactive interface for testing the available API endpoints.

## 🧪 Example Request

### Create an Event

```http
POST /api/events
Content-Type: application/json
```

Request body:

```json
{
  "name": "Technology Conference 2026",
  "description": "Annual technology and innovation conference",
  "startDate": "2026-09-15T09:00:00Z",
  "endDate": "2026-09-17T17:00:00Z",
  "location": "Addis Ababa"
}
```

### Get All Events

```http
GET /api/events
```

Example response:

```json
[
  {
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Technology Conference 2026",
    "description": "Annual technology and innovation conference",
    "startDate": "2026-09-15T09:00:00Z",
    "endDate": "2026-09-17T17:00:00Z",
    "location": "Addis Ababa"
  }
]
```

## 🛣️ Development Roadmap

This project is being developed incrementally while learning modern .NET backend development.

### Completed

* [x] .NET fundamentals
* [x] C# fundamentals
* [x] Collections and Generics
* [x] LINQ
* [x] Lambda Expressions
* [x] Delegates
* [x] Enums and Records
* [x] Async / Await
* [x] Tasks
* [x] CancellationToken
* [x] ASP.NET Core Web API
* [x] PostgreSQL integration
* [x] Entity Framework Core
* [x] Database migrations
* [x] DTOs
* [x] CRUD operations
* [x] Swagger / OpenAPI
* [x] Service Layer

### In Progress

* [ ] Dependency Injection in depth
* [ ] DTO mapping
* [ ] Validation
* [ ] Global error handling
* [ ] Logging
* [ ] Authentication
* [ ] JWT
* [ ] Authorization
* [ ] Entity relationships
* [ ] Pagination
* [ ] Filtering and sorting
* [ ] API versioning
* [ ] Production configuration

### Planned Event Management Features

* [ ] User management
* [ ] Authentication and authorization
* [ ] Event categories
* [ ] Venues
* [ ] Exhibitors
* [ ] Visitors
* [ ] Event registration
* [ ] Tickets
* [ ] Payments
* [ ] Leads
* [ ] Notifications
* [ ] Event dashboard

## 🎯 Project Goals

The main goals of this project are:

1. Build a complete backend using modern .NET.
2. Understand ASP.NET Core Web API architecture.
3. Gain practical experience with Entity Framework Core.
4. Learn PostgreSQL integration.
5. Understand dependency injection and service-based architecture.
6. Implement authentication and authorization.
7. Build a realistic event management domain.
8. Apply production-ready backend development practices.

## 🔐 Security

Sensitive configuration files are excluded from version control.

The repository should never contain:

* Database passwords
* API keys
* JWT secrets
* Access tokens
* Private credentials

Local environment configuration should be maintained separately.

## 📌 Status

**Current status:** Active development

The project currently provides a working Event CRUD API with PostgreSQL persistence, DTOs, Swagger documentation, and a service layer.

More advanced architecture and Event Management features will be added incrementally.
