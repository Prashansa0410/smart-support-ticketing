# SmartSupport Ticket Service

## Overview
SmartSupport.TicketService is a minimal ASP.NET Core Web API project designed to manage support tickets. It provides a simple interface for creating and retrieving tickets, focusing on core ticket functionality without any authentication or machine learning features.

## Project Structure
The project is organized into the following folders and files:

- **Controllers**: Contains the `TicketsController` which handles API endpoints for ticket management.
- **Data**: Contains the `TicketDbContext` class for database interactions using Entity Framework Core.
- **Models**: Contains the `Ticket` class representing the ticket entity.
- **Migrations**: Folder for Entity Framework Core migration files.
- **appsettings.json**: Configuration file for application settings, including the SQLite connection string.
- **Program.cs**: Entry point of the application, setting up services and the HTTP request pipeline.
- **SmartSupport.TicketService.csproj**: Project file containing metadata and dependencies.

## Setup Instructions

1. **Clone the Repository**
   ```bash
   git clone <repository-url>
   cd SmartSupport.TicketService
   ```

2. **Restore Dependencies**
   ```bash
   dotnet restore
   ```

3. **Run Migrations**
   To create the database and apply migrations, run:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the Application**
   Start the application using:
   ```bash
   dotnet run
   ```

5. **Access Swagger UI**
   Once the application is running, you can access the Swagger UI at:
   ```
   http://localhost:5000/swagger
   ```

## API Endpoints

### Create a Ticket
- **POST** `/tickets`
- **Request Body**:
  ```json
  {
    "title": "Ticket Title",
    "description": "Ticket Description",
    "customerEmail": "customer@example.com"
  }
  ```
- **Response**:
  ```json
  {
    "id": "GUID",
    "status": "open",
    "createdAt": "2023-10-01T12:00:00Z"
  }
  ```

### Get All Tickets
- **GET** `/tickets`
- **Response**:
  ```json
  [
    {
      "id": "GUID",
      "title": "Ticket Title",
      "status": "open",
      "createdAt": "2023-10-01T12:00:00Z"
    },
    ...
  ]
  ```

### Get Ticket by ID
- **GET** `/tickets/{id}`
- **Response**:
  ```json
  {
    "id": "GUID",
    "title": "Ticket Title",
    "description": "Ticket Description",
    "customerEmail": "customer@example.com",
    "status": "open",
    "createdAt": "2023-10-01T12:00:00Z"
  }
  ```

## Conclusion
SmartSupport.TicketService provides a clean and minimal implementation for managing support tickets. It is designed for easy setup and usage, making it suitable for development and testing purposes.